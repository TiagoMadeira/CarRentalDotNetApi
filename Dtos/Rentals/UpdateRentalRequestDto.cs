using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Dtos.Rentals
{
    public class UpdateRentalRequestDto
    {   
        [JsonProperty(PropertyName = "start_date")]
        public string StartDate {get; set;}
        [JsonProperty(PropertyName = "end_date")]
        public string EndDate {get; set;}
    }
}