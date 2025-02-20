using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class RentalDto
    {
        public int Id {get; set;}
        public DateOnly StartDate {get; set;}
        public DateOnly EndDate {get; set;}
        public int VehicleId {get; set;}
        public string VehicleDescription {get; set;}
        public string AppUserId {get; set;}
        public string UserName {get; set;}
    }
}