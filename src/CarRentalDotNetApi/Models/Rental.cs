using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;

namespace api.Models
{
    public class Rental
    {
        public int Id {get; set;}
        public int BlockedDateId {get; set;}
        [ForeignKey("BlockedDateId")]
        public BlockedDate BlockedDate {get; set;}
        public int VehicleId {get; set;}
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle {get; set;}
        public string AppUserId {get; set;}
        [ForeignKey("AppUserId")]
        public AppUser AppUser {get; set;}
        public bool Cancelled {get; set;}

        public string State(){
            var state = "Upcoming";
            if(Cancelled)
                state = "Cacelled";
            else if(BlockedDate.StartDate.CompareTo(DateHelperMethods.Today())<=0 && BlockedDate.EndDate.CompareTo(DateHelperMethods.Today())>=0)
                state="Ongoing";
            else if(BlockedDate.EndDate.CompareTo(DateHelperMethods.Today()) < 0)
                state ="Concluded";    
            return state;
        }

        public bool IsUpcoming(){
            return State()=="Upcoming";
        }

        public bool Cancellable(){
            return IsUpcoming();
        }

        public bool RentalVehicleIsAvailable(DateOnly StartDate, DateOnly EndDate)
        {
            return Vehicle.IsVehicleAvailableExcludeRental(StartDate, EndDate, Id);
        }
       
    }
}