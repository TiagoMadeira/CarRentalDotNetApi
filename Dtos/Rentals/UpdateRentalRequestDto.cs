using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Rentals
{
    public class UpdateRentalRequestDto
    {
        public DateOnly StartDate {get; set;}
        public DateOnly EndDate {get; set;}
    }
}