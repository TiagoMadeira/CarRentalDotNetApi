using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Vehicles;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly UserManager<AppUser> _userManager;
        public VehicleController(IVehicleService vehicleService, UserManager<AppUser> userManager)
        {
            _vehicleService = vehicleService;
            _userManager = userManager;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequestDto createVehicleRequestDto){

            var currentUserId = _userManager.GetUserId(User);
            var result = await _vehicleService.CreateAsync(currentUserId, createVehicleRequestDto);
            if (!result.IsSuccess) return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value.ToVehicleDto());
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var result = await _vehicleService.GetByIdAsync(id) ;
            if (!result.IsSuccess) return BadRequest(result.Errors);
   
            return Ok(result.Value.ToVehicleDto());
        }
    }
}