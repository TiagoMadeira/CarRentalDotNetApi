using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class BlockedDate
    {
        public int Id {get; set;}
        public DateOnly StartDate {get; set;} 
        public DateOnly EndDate {get; set;} 
        public Rental? Rental {get; set;}

    }
}