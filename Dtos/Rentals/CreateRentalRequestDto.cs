using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Rentals
{
    public class CreateRentalRequestDto
    {
        public DateOnly StartDate {get; set;}
        public DateOnly EndDate {get; set;}
        public int VehicleId {get; set;}

    }
}