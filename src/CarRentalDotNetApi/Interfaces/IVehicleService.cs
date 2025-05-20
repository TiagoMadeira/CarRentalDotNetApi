using api.Dtos.Rentals;
using api.Dtos.Vehicles;
using api.Helpers;
using api.Models;
using api.Shared;

namespace api.Interfaces
{
    public interface IVehicleService
    {
        Task<Result<Vehicle>> CreateAsync(string userId, CreateVehicleRequestDto createVehicleRequestDto);
        Task<Result<Vehicle>> GetByIdAsync(int Id);
        Task<Result<List<Vehicle>>> FilterAsync(VehicleQueryObject query);
    }
}
