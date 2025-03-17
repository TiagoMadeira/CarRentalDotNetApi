using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Models;

namespace api.Interfaces
{
    public interface IRentalManagerService
    {
        Task<Rental> CreateAsync(string userId, CreateRentalRequestDto createRentalRequestDto);

    }
}