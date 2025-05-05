using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {   
                    Id = "5e438c8e-df84-4f39-80af-82306f2764f9",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "6ac343b0-00ef-4a1c-8f64-68daaca77b5b"
                },
                new IdentityRole
                {   
                    Id = "a51a1291-d383-43f3-9522-cb9324885d11",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "1450449e-a75a-446c-9d00-866ed5f529f7"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
        public  DbSet<Rental> Rentals {get; set;}
        public  DbSet<BlockedDate> BlockedDates {get; set;}
        public  DbSet<Vehicle> Vehicles {get; set;}

    }
}