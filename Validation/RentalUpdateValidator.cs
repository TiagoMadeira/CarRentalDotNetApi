﻿using api.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace api.Validation
{
    public class RentalUpdateValidatorcs : AbstractValidator<Rental>
    {

        public RentalUpdateValidatorcs(BlockedDate NewBlockedDate)
        {
            RuleFor(rental => rental)
            .Must(rental =>
            { return rental.IsUpcoming(); })
            .WithMessage("Rental must be upcoming");
            RuleFor(rental => rental)
            .Must(rental =>
            { return rental.RentalVehicleIsAvailable(NewBlockedDate.StartDate,NewBlockedDate.EndDate); })
            .WithMessage("Vehicle is not available for sugested dates");

        }
    }
}
