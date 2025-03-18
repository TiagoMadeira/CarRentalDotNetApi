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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
         private readonly IVehicleRepository _vehicleRepo;
        public VehicleController( IVehicleRepository vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequestDto createVehicleRequestDto){
             var  userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId !=null){
                var vehicleModel = createVehicleRequestDto.ToVehicleFromCreateRequestDto(userId);
                var vehicle = await _vehicleRepo.CreateAsync(vehicleModel);

                return CreatedAtAction(nameof(GetById), new {id = vehicle.Id}, vehicle.ToVehicleDto());
            }
            return UnprocessableEntity();
        }

        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            var vehicle = await _vehicleRepo.GetByIdAsync(id) ;
            if(vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle.ToVehicleDto());
        }
    }
}