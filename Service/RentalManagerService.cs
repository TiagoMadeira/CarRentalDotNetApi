using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Interfaces;
using api.Mappers;
using api.Models;

namespace api.Service
{
    public class RentalManagerService : IRentalManagerService
    {   
        private readonly IRentalRepository _rentalRepo;
        private readonly IBlockedDateRepository _blockedDateRepo;
        public RentalManagerService(IRentalRepository rentalRepo )
        {
            _rentalRepo = rentalRepo;
        }

        public async Task<Rental> CreateAsync(string userId, CreateRentalRequestDto createRentalRequestDto)
        {
            var blockedDate = createRentalRequestDto.ToBlockedDateFromCreateRequestDto();
            await _blockedDateRepo.CreateAsync(blockedDate);
            var rentalModel = createRentalRequestDto.ToRentalFromCreateRequestDto( blockedDate.Id, userId);
            await _rentalRepo.CreateAsync(rentalModel);
            return rentalModel;

        }

        public Task<Rental?> UpdateAsync(int id, UpdateRentalRequestDto updateRentalRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}