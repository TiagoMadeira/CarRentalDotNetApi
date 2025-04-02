using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using FluentValidation;

namespace api.Validation.InputValidators.cs
{
    public class LoginDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("Wrong email format");

            RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull().WithMessage("Password is required");
        }
    }
}