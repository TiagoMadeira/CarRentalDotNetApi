using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos
{
    public class RentalDto
    {
        public int Id {get; set;}
        public string StartDate {get; set;}
        public string EndDate {get; set;}
        public int VehicleId {get; set;}
        public string VehicleDescription {get; set;}
        public string UserId {get; set;}
        public string UserName {get; set;}
    }
}