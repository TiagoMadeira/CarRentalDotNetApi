using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Models;

namespace api.Mappers
{
    public static class BlockedDateMappers
    {
         public static BlockedDate ToBlockedDateFromCreateRequestDto(this CreateRentalRequestDto createRentalRequestDto)
       {
        return new BlockedDate
        {
            StartDate=createRentalRequestDto.StartDate,
            EndDate=createRentalRequestDto.EndDate,
        };
       }
    }
}