using api.Dtos.Rentals;
using api.Dtos.Vehicles;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Shared;
using api.Validation;
using FluentValidation.Results;

namespace api.Service
{
    public class vehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepo;

        public vehicleService(IVehicleRepository vehicleRepo)
        {
            _vehicleRepo = vehicleRepo;
        }

        public async Task<Result<Vehicle>> CreateAsync(string userId, CreateVehicleRequestDto createVehicleRequestDto)
        {
            var buildVehicleResult = BuildVehicle(userId, createVehicleRequestDto);
            if (!buildVehicleResult.IsSuccess)
                return Result<Vehicle>.Failure(buildVehicleResult.Errors);

            var vehicleModel = await _vehicleRepo.CreateAsync(buildVehicleResult.Value);
            return Result<Vehicle>.Success(vehicleModel);
        }

        public Task<Result<Vehicle>> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }


        private Result<Vehicle> BuildVehicle( string userId, CreateVehicleRequestDto createVehicleRequestDto)
        {
            var vehicleModel = createVehicleRequestDto.ToVehicleFromCreateRequestDto(userId);
            var result = ValidateVehicle(vehicleModel).Result;

            if (!result.IsValid)
                return Result<Vehicle>.Failure(new Errors(result.ToDictionary(), "Rental Build Error"));

            return Result<Vehicle>.Success(vehicleModel);
        }

        private async Task<ValidationResult> ValidateVehicle(Vehicle vehicleModel)
        {
            var VehicleValidator = new VehicleValidator();
            return await VehicleValidator.ValidateAsync(vehicleModel);
        }
    }
}
