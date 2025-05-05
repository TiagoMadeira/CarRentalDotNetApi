using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Models;
using api.Shared;

namespace api.Interfaces
{
    public interface IRentalManagerService
    {
        Task<Result<Rental>> CreateAsync(string userId, CreateRentalRequestDto createRentalRequestDto);
        Task<Result<Rental>> GetByIdAsync(int Id);
        Task<Result<Rental>> CancelAsync(int Id);
        Task<Result<Rental>> UpdateAsync(int Id, UpdateRentalRequestDto updateRentalRequestDto);
    }
}