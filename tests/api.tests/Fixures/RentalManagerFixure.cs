using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace api.tests.Fixures
{
    public class DatabaseFixture : IDisposable
    {
        // Constructor: Set up your fixture.
        public DatabaseFixture()
        {
            //This should be a testdb connection
            Db = new SqlConnection("DefaultConnection");
        }

        // IDisposable implementation: Clean up your fixture.
        public void Dispose()
        {
            // Dispose of or release any resources used by your fixture.
        }

        private void setUpFixure(SqlConnection db)
        {
            var validBlocked = new BlockedDate
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(2))
            };

            var validCar = new Vehicle
            {
                Model = "Test Model",
                Brand = "Test Brand"
            };
        }

        public SqlConnection Db { get; private set; }
    }
}
