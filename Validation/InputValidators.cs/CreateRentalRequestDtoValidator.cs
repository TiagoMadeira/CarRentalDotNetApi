using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rentals;
using api.Interfaces;
using FluentValidation;

namespace api.Validation.InputValidators.cs
{
    public class CreateRentalRequestDtoValidator : AbstractValidator<CreateRentalRequestDto>
    {
        public CreateRentalRequestDtoValidator(IVehicleRepository vehicleRepo)
        {
            RuleFor(x => x.StartDate)
            .NotEmpty()
            .NotNull().WithMessage("StartDate is required");
        
            RuleFor(x => x.EndDate)
            .NotEmpty()
            .NotNull().WithMessage("EndDate is required");

            RuleFor(x => x.VehicleId)
            .NotEmpty()
            .NotNull().WithMessage("VehicleId is required")
            .MustAsync(async (id, _ ) => 
            {return await vehicleRepo.VehicleExistsAsync(id);})
            .WithMessage("Email already in use");
        }
    }
}
