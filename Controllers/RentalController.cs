using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{   [Route("api/rental")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public RentalController(ApplicationDBContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id){
            var rental = _context.Rentals.Find(id);
            if(rental == null)
            {
                return NotFound();
            }
            return Ok(rental.ToRentalDto());
        }
    }
} 