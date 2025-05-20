using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;

namespace api.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(int id);

        Task<Vehicle> CreateAsync(Vehicle vehicleModel);

        Task<List<Vehicle>> FilterVehiclesAsync(VehicleQueryObject vehicleModel);

        Task<bool> VehicleExistsAsync(int id);

        Task<bool> VehicleIsAvailableAsync(int id, DateOnly StartDate, DateOnly EndDate);
    }
}