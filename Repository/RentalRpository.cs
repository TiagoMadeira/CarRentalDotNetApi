using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Rentals;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Rentals.Include(r => r.BlockedDate)
                                        .Include(r => r.Vehicle)
                                        .Include(r => r.AppUser)
                                        .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Rental> CreateAsync(Rental rentalModel)
        {
            await _context.Rentals.AddAsync(rentalModel);
            await _context.SaveChangesAsync();
            return rentalModel;
        }

        public async Task<Rental?> DeleteAsync(int id)
        {
            var rentalModel = await _context.Rentals.FirstOrDefaultAsync(x => x.Id == id);
            if(rentalModel == null)
                return null;
            _context.Rentals.Remove(rentalModel);
            await _context.SaveChangesAsync();
            return rentalModel;

        }   
    }
}