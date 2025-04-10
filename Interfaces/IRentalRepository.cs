using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Models;

namespace api.Interfaces
{
    public interface IRentalRepository
    {
        Task<Rental?> GetByIdAsync(int id);

        Task<Rental> CreateAsync(Rental rentalModel);

        Task<Rental> CancelAsync(Rental existingRental);

        Task<bool>RentalExistsAsync(int id);
    }
}