using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {   
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public List<Rental>? Rentals {get; set;}

    }
}