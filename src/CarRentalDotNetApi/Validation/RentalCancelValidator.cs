using api.Models;
using FluentValidation;

namespace api.Validation
{
    public class RentalCancelValidator :AbstractValidator<Rental>
    {
        public RentalCancelValidator()
        {
            RuleFor(rental => rental)
           .Must(rental =>
           { return rental.IsUpcoming(); })
           .WithMessage("Rental must be upcoming");
        }
    }
}
