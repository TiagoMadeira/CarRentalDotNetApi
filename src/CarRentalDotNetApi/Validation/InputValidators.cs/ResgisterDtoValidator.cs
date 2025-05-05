using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Models;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace api.Validation.InputValidators.cs
{
    public class ResgisterDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public ResgisterDtoValidator(UserManager<AppUser> userManager)
        {
           RuleFor(x => x.Username)
           .NotEmpty()
           .NotNull().WithMessage("Username is Required");

           RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("Wrong email format")
            .Must(email => 
            {return userManager.Users.Any(u => u.Email == email);})
            .WithMessage("Email already in use");

            RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull().WithMessage("Password is required");
        }
    }
}