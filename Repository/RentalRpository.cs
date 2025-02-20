using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repository
{
    public class RentalRpository : IRentalRepository
    {
        private readonly ApplicationDBContext _context;
        public RentalRpository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Rental?> GetByIdAsync(int id)
        {
            return await _context.Rentals.FindAsync(id);
        }
    }
}