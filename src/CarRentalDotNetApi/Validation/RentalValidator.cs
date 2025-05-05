using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using FluentValidation;

namespace api.Validation
{
    public class RentalValidator :   AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(rental => rental.VehicleId).NotNull()
            .WithMessage("Rental must have a vehicle");

            RuleFor(rental => rental.BlockedDateId).NotNull()
            .WithMessage("Rental must have blockedDates");

        }
    }
}