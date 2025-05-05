using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Shared;
using api.Validation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;

namespace api.Service
{
    public class RentalManagerService : IRentalManagerService
    {
        private readonly IRentalRepository _rentalRepo;
        private readonly IBlockedDateRepository _blockedDateRepo;
        private readonly IVehicleRepository _vehicleRepo;
        public RentalManagerService(IRentalRepository rentalRepo, IBlockedDateRepository blockedDateRepo, IVehicleRepository vehicleRepo)
        {
            _rentalRepo = rentalRepo;
            _blockedDateRepo = blockedDateRepo;
            _vehicleRepo = vehicleRepo;
        }

        public async Task<Result<Rental>> CreateAsync(string userId, CreateRentalRequestDto createRentalRequestDto)
        {
            //Validate/Build Blocked Date
            var blockedDatesBuildResult = BuildBlockedDate(createRentalRequestDto);
            if (!blockedDatesBuildResult.IsSuccess)
                return Result<Rental>.Failure(blockedDatesBuildResult.Errors);

            var blockedDate = blockedDatesBuildResult.Value;

            //Validate Vehicle
            if (!await _vehicleRepo.VehicleExistsAsync(createRentalRequestDto.VehicleId))
                return Result<Rental>.Failure(RentaltErrors.RentalVehicleDoesNotExist);

            if (!await _vehicleRepo.VehicleIsAvailableAsync(createRentalRequestDto.VehicleId, blockedDate.StartDate, blockedDate.EndDate ))
                return Result<Rental>.Failure(RentaltErrors.RentalVehicleIsNotAvailable);

            //Build Rental
            var BuildRentalresult = BuildRental(blockedDatesBuildResult.Value.Id, userId, createRentalRequestDto);
            if (!BuildRentalresult.IsSuccess)
                return Result<Rental>.Failure(BuildRentalresult.Errors);

            var rentalModel = BuildRentalresult.Value;

            //Create BlockedDate
            await _blockedDateRepo.CreateAsync(blockedDate);
            //Create Rental
            await _rentalRepo.CreateAsync(rentalModel);
            return Result<Rental>.Success(rentalModel);
        }

        public async Task<Result<Rental>> GetByIdAsync(int Id)
        {
            var rental = await _rentalRepo.GetByIdAsync(Id);
            if (rental == null)
                return Result<Rental>.Failure(RentaltErrors.RentalDoesNotExist);
            return Result<Rental>.Success(rental);
        }

        public async Task<Result<Rental>> UpdateAsync(int Rentalid, UpdateRentalRequestDto updateRentalRequestDto)
        {
            var rental = await _rentalRepo.GetByIdAsync(Rentalid);
            if (rental == null)
                return Result<Rental>.Failure(RentaltErrors.RentalDoesNotExist);

            //Validate BlockDate data
            var blockedDate = updateRentalRequestDto.ToBlockedDateFromUpdateRequestDto();
            var BlockedDateValidationResult = ValidateBlockedDate(blockedDate);
            if (!BlockedDateValidationResult.IsValid)
                return Result<Rental>.Failure(new Errors(BlockedDateValidationResult.ToDictionary(), "BlockedDate build error"));

            //Validate Rental State
            var RentalUpdateValidation = ValidateRentalUpdate(blockedDate, rental);
            if (!RentalUpdateValidation.IsValid)
                return Result<Rental>.Failure(new Errors(RentalUpdateValidation.ToDictionary(), "Rental chould not be updated"));

            //Update
            await _blockedDateRepo.UpdateAsync(rental.BlockedDateId, blockedDate);
            return Result<Rental>.Success(rental);
        }

        public async Task<Result<Rental>> CancelAsync(int id)
        {
            var rental = await _rentalRepo.GetByIdAsync(id);

            if (rental == null)
                return Result<Rental>.Failure(RentaltErrors.RentalDoesNotExist);

            var cancelValidation = ValidateRentalCancel(rental);
            if (!cancelValidation.IsValid)
                return Result<Rental>.Failure(new Errors(cancelValidation.ToDictionary(), "Rental Cancel Errors"));

            await _rentalRepo.CancelAsync(rental);
            return Result<Rental>.Success(rental);

        }

        private ValidationResult ValidateBlockedDate(BlockedDate blockedDate)
        {
            var blockedDateValidator = new BlockedDateValidator();
            return blockedDateValidator.Validate(blockedDate);
        }

        private Result<BlockedDate> BuildBlockedDate(CreateRentalRequestDto createRentalRequestDto)
        {
            var blockedDate = createRentalRequestDto.ToBlockedDateFromCreateRequestDto();
            var result = ValidateBlockedDate(blockedDate);

            if (!result.IsValid)
                return Result<BlockedDate>.Failure(new Errors(result.ToDictionary(), "BlockedDate build Error"));

            return Result<BlockedDate>.Success(blockedDate);
        }

        private async Task<ValidationResult> ValidateRental(Rental rental)
        {
            var rentalValidator = new RentalValidator();
            return await rentalValidator.ValidateAsync(rental);
        }

        private Result<Rental> BuildRental(int BlockeddateId, string userId, CreateRentalRequestDto createRentalRequestDto)
        {
            var rentalModel = createRentalRequestDto.ToRentalFromCreateRequestDto(BlockeddateId, userId);
            var result =  ValidateRental(rentalModel).Result;

            if (!result.IsValid)
                return Result<Rental>.Failure(new Errors(result.ToDictionary(), "Rental Build Error"));

            return Result<Rental>.Success(rentalModel);
        }
        private ValidationResult ValidateRentalUpdate(BlockedDate NewBlockedDate, Rental rental)
        {
            var rentalUpdateValidator = new RentalUpdateValidator(NewBlockedDate);
            return rentalUpdateValidator.Validate(rental);
        }
        private ValidationResult ValidateRentalCancel(Rental rental)
        {
            var rentalUpdateValidator = new RentalCancelValidator();
            return rentalUpdateValidator.Validate(rental);
        }
    }
}