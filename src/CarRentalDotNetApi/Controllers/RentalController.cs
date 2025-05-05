using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Rentals;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Service;
using api.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{   [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {   
        private readonly IRentalManagerService _rentalManager;
        public RentalController(  IRentalManagerService rentalManager, UserManager<AppUser> userManager)
        {
            _rentalManager = rentalManager;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalRequestDto createRentalRequestDto, IValidator<CreateRentalRequestDto> validator){
            var result = await validator.ValidateAsync(createRentalRequestDto);
            if (!result.IsValid)
                return BadRequest(result.ToDictionary());
       
            var  userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var createResult = await _rentalManager.CreateAsync(userId, createRentalRequestDto);
            if(!createResult.IsSuccess)
                return BadRequest(createResult.Errors);

            return CreatedAtAction(nameof(GetById), new {id = createResult.Value.Id}, createResult.Value.ToRentalDto());
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var result = await _rentalManager.GetByIdAsync(id) ;
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
     
            return Ok(result.Value.ToRentalDto());
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRentalRequestDto updateRentalRequestDto, IValidator<UpdateRentalRequestDto> validator){
            validator.ValidateAndThrow(updateRentalRequestDto);
            var result = await _rentalManager.UpdateAsync(id,updateRentalRequestDto );
            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result.Value.ToRentalDto());
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Cancel([FromRoute] int id){
            
            var result = await _rentalManager.CancelAsync(id );
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result.Value.ToRentalDto());
        }
    }
} 