using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Helpers
{
    public static class DateHelperMethods
    {
        public static Boolean AreOverlapping(DateOnly startDate1, DateOnly endDate1, DateOnly startDate2, DateOnly endDate2 )
        {
            return startDate1 < endDate2 && startDate2 < endDate1;
        }

        public static DateOnly Today(){
            return DateOnly.FromDateTime(DateTime.Now);
        }
    }
}
    
