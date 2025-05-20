using api.Helpers;
using api.Models;
using FluentValidation;

namespace api.Validation
{
    public class VehicleQueryValidator : AbstractValidator<VehicleQueryObject>
    {
        public VehicleQueryValidator()
        {
            RuleFor(query => query.Model)
            .NotEmpty().WithMessage("Model field cannot be empty string");

            RuleFor(query => query.Brand)
           .NotEmpty().WithMessage("Brand field cannot be empty string");

            RuleFor(query => query.Category)
            .NotEmpty().WithMessage("Category Field cannot be empty string")
            .Must((Category) => { return Enum.IsDefined(typeof(categoryNames), Category); });

            RuleFor(query => query.Transmission)
            .NotEmpty().WithMessage("Transmission Field cannot be empty string")
            .Must((Transmission) => { return Enum.IsDefined(typeof(transmissionNames), Transmission); });

            RuleFor(query => query.VehicleType)
            .NotEmpty().WithMessage("VehicleType Field cannot be empty string")
            .Must((VehicleType) => { return Enum.IsDefined(typeof(vehicleTypeNames), VehicleType); });
        }
    }
}
