using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDBContext _context;

        public VehicleRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> CreateAsync(Vehicle vehicleModel)
        {
            await _context.Vehicles.AddAsync(vehicleModel);
            await _context.SaveChangesAsync();
            return vehicleModel;
        }
        

        public async Task<Vehicle?> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id);
        }

        public async Task<bool> VehicleExistsAsync(int id)
        {
            return await _context.Vehicles.AnyAsync(v => v.Id == id);
        }

        public async Task<bool> VehicleIsAvailableAsync(int id, DateOnly StartDate, DateOnly EndDate)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
                return vehicle.IsVehicleAvailable(StartDate, EndDate);
            return false;
        }   
           
    }
}