using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using FluentValidation;

namespace api.Validation
{
    public class BlockedDateValidator: AbstractValidator<BlockedDate>
    {
        public BlockedDateValidator()
        {
             RuleFor(blockedDate => blockedDate.StartDate).NotNull().NotEmpty()
            .WithMessage("BlockedDate must have StartDate");

            RuleFor(blockedDate => blockedDate.EndDate).NotNull().NotEmpty()
            .WithMessage("BlockedDate must have EndDate");

           RuleFor(blockedDate => blockedDate.StartDate)
           .Must((blockedDate, StartDate) => {return StartDate.CompareTo(DateOnly.FromDateTime(DateTime.Now))>0;})
           .WithMessage("StartDate must be set in the future");

           RuleFor(blockedDate => blockedDate.EndDate)
           .Must((blockedDate, EndDate) => {return EndDate.CompareTo(blockedDate.StartDate)>0;})
           .WithMessage("EndDate must be after StartDate");
        }
    }
}