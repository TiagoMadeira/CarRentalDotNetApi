using System.Reflection;
using api.Dtos.Rentals;
using api.Dtos.Vehicles;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Shared;
using api.Validation;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json.Linq;

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

        public async Task<Result<List<Vehicle>>> FilterAsync(VehicleQueryObject query)
        {

            //Validate query
            var validationResult = ValidateVehicleQuery(query).Result;
            if (!validationResult.IsValid)
                return Result<List<Vehicle>>.Failure(new Errors(validationResult.ToDictionary(), "Vehicle Query Validation Error")); 

            var vehicles = await _vehicleRepo.FilterVehiclesAsync(query);

            return Result<List<Vehicle>>.Success(vehicles);

        }

        public async Task<Result<Vehicle>> GetByIdAsync(int Id)
        {
            var vehicle = await _vehicleRepo.GetByIdAsync(Id);
            if (vehicle == null) return Result<Vehicle>.Failure(VehicleErrors.VehicleDoesExistError);

            return Result<Vehicle>.Success(vehicle);
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
        private async Task<ValidationResult> ValidateVehicleQuery(VehicleQueryObject query)
        {
            var VehicleQueryValidator = new VehicleQueryValidator();
            return await VehicleQueryValidator.ValidateAsync(query);
        }
    }
}
}
