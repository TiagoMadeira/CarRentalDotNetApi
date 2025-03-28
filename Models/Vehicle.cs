using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
namespace api.Models
{
    public enum categoryNames {
        small, 
        medium, 
        big, 
        suv, 
        truck
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
        public categoryNames? Category {get; set;}
        public transmissionNames? Transmission {get; set;}
        public vehicleTypeNames? VehicleType {get; set;}
        public List<Rental>? Rentals {get; set;}
        public string AppUserId {get; set;}
        [ForeignKey("AppUserId")]
        public AppUser AppUser {get; set;}

        public Boolean IsVehicleAvailable(DateOnly StartDate, DateOnly EndDate){
            if (Rentals == null){
                return true;
            }
            foreach (Rental rental in Rentals){
                if (DateHelperMethods.AreOverlapping(StartDate, EndDate, rental.BlockedDate.StartDate, rental.BlockedDate.EndDate)){
                    return false;
                }
            }
            return true;
            
        }
    }
}