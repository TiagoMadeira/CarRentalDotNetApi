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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{   [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {   
        private readonly UserManager<AppUser> _userManager;
        private readonly IRentalRepository _rentalRepo;
        private readonly IRentalManagerService _rentalManager;
        public RentalController( IRentalRepository rentalRepo, IRentalManagerService rentalManager, UserManager<AppUser> userManager)
        {
            _rentalRepo = rentalRepo;
            _rentalManager = rentalManager;
            _userManager = userManager;

        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateRental([FromBody] CreateRentalRequestDto createRentalRequestDto){
            var  userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rental = await _rentalManager.CreateAsync(userId, createRentalRequestDto);
            return CreatedAtAction(nameof(GetById), new {id = rental.Id}, rental.ToRentalDto());
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var rental = await _rentalRepo.GetByIdAsync(id) ;
            if(rental == null)
            {
                return NotFound();
            }
            return Ok(rental.ToRentalDto());
        }
    }
} 