using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos;
using api.Dtos.Rentals;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers
{
    public static class RentalMappers
    {
       public static RentalDto ToRentalDto(this Rental rentalModel)
       {
        return new RentalDto
        {
            Id = rentalModel.Id,
            StartDate= rentalModel.BlockedDate.StartDate.ToString(),
            EndDate=rentalModel.BlockedDate.EndDate.ToString(),
            VehicleId = rentalModel.VehicleId,
            VehicleDescription = string.Format("{0} {1} {2}", 
                                    rentalModel.Vehicle.Brand.ToString(),
                                    rentalModel.Vehicle.Model.ToString(),
                                    rentalModel.Vehicle.VehicleType.ToString()),
            UserId = rentalModel.AppUserId,
            UserName = rentalModel.AppUser.UserName
        };
       } 

        public static Rental ToRentalFromCreateRequestDto(this CreateRentalRequestDto createRentalRequestDto, int blockedDateId, string userId)
        {
            return new Rental{
                VehicleId = createRentalRequestDto.VehicleId,
                AppUserId = userId,
                BlockedDateId = blockedDateId
            };
        }
    }
}