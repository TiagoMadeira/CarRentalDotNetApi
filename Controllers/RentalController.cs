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
            await validator.ValidateAndThrowAsync(createRentalRequestDto);
            var  userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rental = await _rentalManager.CreateAsync(userId, createRentalRequestDto);
            return CreatedAtAction(nameof(GetById), new {id = rental.Id}, rental.ToRentalDto());
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var rental = await _rentalManager.GetByIdAsync(id) ;
            if(rental == null)
            {
                return NotFound();
            }
            return Ok(rental.ToRentalDto());
        }

        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRentalRequestDto updateRentalRequestDto, IValidator<UpdateRentalRequestDto> validator){
            validator.ValidateAndThrow(updateRentalRequestDto);
            var rental = await _rentalManager.UpdateAsync(id,updateRentalRequestDto );
            
            return Ok(rental.ToRentalDto());
        }
    }
} 