using api.Models;
using FluentValidation;


namespace api.Validation
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {

        public VehicleValidator() {
            RuleFor(vehicle => vehicle.AppUserId).NotNull().NotEmpty()
            .WithMessage("Vehicle must have an associated AppUserId");

            RuleFor(vehicle => vehicle.Model).NotNull().NotEmpty()
            .WithMessage("vehcile must have brand");

            RuleFor(vehicle => vehicle.Brand).NotNull().NotEmpty()
           .WithMessage("vehcile must have brand");
        }
    }
}
