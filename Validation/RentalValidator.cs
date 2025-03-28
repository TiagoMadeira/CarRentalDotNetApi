using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            RuleFor(rental => rental.Vehicle).NotNull()
            .WithMessage("Rental must have a vehicle");

            RuleFor(rental => rental.BlockedDateId).NotNull()
            .WithMessage("Rental must have blockedDates");

            RuleFor(rental => rental.BlockedDate).NotNull()
            .WithMessage("Rental must have blockedDates");

            RuleFor(rental => rental.Vehicle)
            .Must((rental, vehicle) => {return vehicle.IsVehicleAvailable(rental.BlockedDate.StartDate, rental.BlockedDate.EndDate) == true;})
            .WithMessage("Vehicle is not available for iserted blockedDates");
        }
    }
}