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
            Category = vehicleModel.Category,
            Transmission = vehicleModel.Transmission,
            VehicleType = vehicleModel.VehicleType,
            AppUserId = vehicleModel.AppUserId
          
        };
       }
       public static Vehicle ToVehicleFromCreateRequestDto(this CreateVehicleRequestDto createVehicleRequestDto, string userId)
        {
            return new Vehicle{
                Model = createVehicleRequestDto.Model,
                Brand = createVehicleRequestDto.Brand,
                Category = createVehicleRequestDto.Category,
                Transmission = createVehicleRequestDto.Transmission,
                VehicleType = createVehicleRequestDto.VehicleType,
                AppUserId = userId
            };
        } 
    }
}