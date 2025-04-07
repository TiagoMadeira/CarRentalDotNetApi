using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Validation;

namespace api.Service
{
    public class RentalManagerService : IRentalManagerService
    {   
        private readonly IRentalRepository _rentalRepo;
        private readonly IBlockedDateRepository _blockedDateRepo;
        public RentalManagerService(IRentalRepository rentalRepo, IBlockedDateRepository blockedDateRepo )
        {
            _rentalRepo = rentalRepo;
            _blockedDateRepo = blockedDateRepo;
        }

        public async Task<Rental> CreateAsync(string userId, CreateRentalRequestDto createRentalRequestDto)
        {   
            //Build Blocked Date
            var blockedDate = createRentalRequestDto.ToBlockedDateFromCreateRequestDto();
            ValidateBlockedDate(blockedDate);
            await _blockedDateRepo.CreateAsync(blockedDate);

            //Build Rental
            var rentalModel = createRentalRequestDto.ToRentalFromCreateRequestDto( blockedDate.Id, userId);
            ValidateRental(rentalModel);
            await _rentalRepo.CreateAsync(rentalModel);
            
            return await  _rentalRepo.GetByIdAsync(rentalModel.Id);
        }

        public async Task<Rental?> GetByIdAsync(int Id){
            return await _rentalRepo.GetByIdAsync(Id);
        }
        public async Task<Rental?> UpdateAsync(int id, UpdateRentalRequestDto updateRentalRequestDto)
        {
            var rental = await _rentalRepo.GetByIdAsync(id);
            if(rental == null){
                throw new BadHttpRequestException("Rental does not exist");
            }
            var blockedDate = updateRentalRequestDto.ToBlockedDateFromUpdateRequestDto();
            ValidateBlockedDate(blockedDate);
            await _blockedDateRepo.UpdateAsync(rental.BlockedDateId, blockedDate);
            return rental;
        }

        private void ValidateBlockedDate(BlockedDate blockedDate){
            var blockedDateValidator = new BlockedDateValidator();
            var validationResult = blockedDateValidator.Validate(blockedDate);

            if(!validationResult.IsValid)
                throw new BadHttpRequestException(validationResult.ToString());

        }

        private void ValidateRental(Rental rental){
            var rentalValidator = new RentalValidator();
            var validationResult = rentalValidator.Validate(rental);

            if(!validationResult.IsValid)
                throw new BadHttpRequestException(validationResult.ToString());
        }
    }
}