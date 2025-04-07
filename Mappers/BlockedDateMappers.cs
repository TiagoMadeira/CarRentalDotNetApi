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
            StartDate = DateOnly.Parse(createRentalRequestDto.StartDate),
            EndDate = DateOnly.Parse(createRentalRequestDto.EndDate),
        };
       }

        public static BlockedDate ToBlockedDateFromUpdateRequestDto(this UpdateRentalRequestDto updateRentalRequestDto)
       {
        return new BlockedDate
        {
            StartDate = DateOnly.Parse(updateRentalRequestDto.StartDate),
            EndDate = DateOnly.Parse(updateRentalRequestDto.EndDate),
        };
       }
    }
}