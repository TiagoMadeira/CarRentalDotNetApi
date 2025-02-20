using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Rental
    {
        public int Id {get; set;}
        public int BlockedDateId {get; set;}
        public BlockedDate BlockedDate {get; set;}

    }
}