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
            var blockedDate = BuildBlockedDate(createRentalRequestDto);
            await _blockedDateRepo.CreateAsync(blockedDate);

            //Build Rental
            var rentalModel = BuildRental(createRentalRequestDto,  blockedDate.Id, userId);
            await _rentalRepo.CreateAsync(rentalModel);
            
            return await  _rentalRepo.GetByIdAsync(rentalModel.Id);
        }

        public Task<Rental?> UpdateAsync(int id, UpdateRentalRequestDto updateRentalRequestDto)
        {
            throw new NotImplementedException();
        }

        private static BlockedDate BuildBlockedDate(CreateRentalRequestDto createRentalRequestDto)
        {
            var blockedDate = createRentalRequestDto.ToBlockedDateFromCreateRequestDto();
            var blockedDateValidator = new BlockedDateValidator();
            var validationResult = blockedDateValidator.Validate(blockedDate);

            if(!validationResult.IsValid)
                throw new BadHttpRequestException(validationResult.ToString());

             return blockedDate;
        }

        private static Rental BuildRental(CreateRentalRequestDto createRentalRequestDto, int BlockedDateId, string userId)
        {
            var rentalModel = createRentalRequestDto.ToRentalFromCreateRequestDto( BlockedDateId, userId);
            var rentalValidator = new RentalValidator();
            var validationResult = rentalValidator.Validate(rentalModel);

            if(!validationResult.IsValid)
                throw new BadHttpRequestException(validationResult.ToString());

             return rentalModel;
        }

       

        


    }
}