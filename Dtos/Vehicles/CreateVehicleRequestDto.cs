using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Dtos.Vehicles
{
    public class CreateVehicleRequestDto
    {
        public string Model {get; set;}
        public string Brand {get; set;}
        public string Category {get; set;}
        public string Transmission {get; set;}
        public string VehicleType {get; set;}
    }
}