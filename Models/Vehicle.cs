using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public enum categoryNames {
        small, maedium, big, suv, truck
    }
    public enum transmissionNames{
        manual, auto
    }
    public enum vehicleTypeNames{
        car, motorcycle, ev, evcycle
    }
    public class Vehicle
    {
          public int Id {get; set;}
          public string Model {get; set;}
          public string Brand {get; set;}
          public categoryNames Category {get; set;}
          public transmissionNames Transmission {get; set;}
          public vehicleTypeNames VehicleType {get; set;}
          public List<Rental>? Rentals {get; set;}



    }
}