using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

    }
}