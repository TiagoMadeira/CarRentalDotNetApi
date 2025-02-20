using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;

namespace api.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {   
            
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<CascadeDeleteConvention>();
            base.ConfigureConventions(modelBuilder);
        }
        public  DbSet<Rental> Rentals {get; set;}
        public  DbSet<BlockedDate> BlockedDates {get; set;}
        public  DbSet<Vehicle> Vehicles {get; set;}

    }
}