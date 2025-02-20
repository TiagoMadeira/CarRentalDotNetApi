using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{   [Route("api/rental")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalRepository _rentalRepo;
        public RentalController( IRentalRepository rentalRepo)
        {
            _rentalRepo = rentalRepo;

        }
        [HttpGet("{id}")]
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