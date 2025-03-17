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
            StartDate=rentalModel.BlockedDate.StartDate,
            EndDate=rentalModel.BlockedDate.EndDate,
            VehicleId = rentalModel.VehicleId,
            VehicleDescription = string.Format("{0} {1} {2}", 
                                    rentalModel.Vehicle.Brand,
                                    rentalModel.Vehicle.Model,
                                    rentalModel.Vehicle.VehicleType),
            UserId = rentalModel.AppUserId,
            UserName = rentalModel.AppUser.Name
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