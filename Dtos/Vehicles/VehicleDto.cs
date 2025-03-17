using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Vehicles
{
    public class VehicleDto
    {
        public int Id {get; set;}
        public string Model {get; set;}
        public string Brand {get; set;}
        public categoryNames Category {get; set;}
        public transmissionNames Transmission {get; set;}
        public vehicleTypeNames VehicleType {get; set;}
        public string AppUserId {get; set;}
    }
}