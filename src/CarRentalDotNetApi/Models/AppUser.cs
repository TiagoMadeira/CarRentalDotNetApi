using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {  
        public string Name {get; set;}
        public string Email { get; set; }
        public List<Rental>? Rentals {get; set;}
        public List<Vehicle>? Vehicles {get; set;}
    }
}