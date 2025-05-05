using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Vehicles;
using api.Models;

namespace api.Mappers
{
    public static class VehicleMappers
    {
        public static VehicleDto ToVehicleDto(this Vehicle vehicleModel)
        {
        return new VehicleDto
        {
            Id = vehicleModel.Id,
            Model = vehicleModel.Model,
            Brand = vehicleModel.Brand,
            Category = vehicleModel?.Category?.ToString(),
            Transmission = vehicleModel?.Transmission?.ToString(),
            VehicleType = vehicleModel?.VehicleType?.ToString(),
            AppUserId = vehicleModel.AppUserId
          
        };
       }
       public static Vehicle ToVehicleFromCreateRequestDto(this CreateVehicleRequestDto createVehicleRequestDto, string userId)
        {
            return new Vehicle{
                Model = createVehicleRequestDto.Model,
                Brand = createVehicleRequestDto.Brand,
                Category = (categoryNames) Enum.Parse(typeof(categoryNames), createVehicleRequestDto.Category),
                Transmission = (transmissionNames) Enum.Parse(typeof(transmissionNames), createVehicleRequestDto.Transmission),
                VehicleType = (vehicleTypeNames) Enum.Parse(typeof(vehicleTypeNames), createVehicleRequestDto.VehicleType),
                AppUserId = userId
            };
        } 
    }
}