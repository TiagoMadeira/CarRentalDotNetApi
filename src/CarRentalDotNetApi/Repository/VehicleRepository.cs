using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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

        public async Task<List<Vehicle>> FilterVehiclesAsync(VehicleQueryObject query)
        {
            var vehicles =  _context.Vehicles.AsQueryable();
            
            if(!string.IsNullOrWhiteSpace(query.Brand))
                vehicles.Where(v => v.Brand.Contains(query.Brand));

            if (!string.IsNullOrWhiteSpace(query.Model))
                vehicles.Where(v => v.Model.Contains(query.Model));

            if (!string.IsNullOrWhiteSpace(query.Category))
                vehicles.Where(v => v.Category.ToString().Equals(query.Category));

            if (!string.IsNullOrWhiteSpace(query.Transmission))
                vehicles.Where(v => v.Transmission.ToString().Equals(query.Transmission));

            if (!string.IsNullOrWhiteSpace(query.VehicleType))
                vehicles.Where(v => v.VehicleType.ToString().Equals(query.VehicleType));

            return await vehicles.ToListAsync();

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