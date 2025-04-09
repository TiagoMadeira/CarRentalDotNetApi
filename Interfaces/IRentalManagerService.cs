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
        Task<Rental?> GetByIdAsync(int Id);
        Task<Rental?> UpdateAsync(int Id, UpdateRentalRequestDto updateRentalRequestDto);

        
    }
}