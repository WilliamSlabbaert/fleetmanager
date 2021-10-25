using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class FuelCardChauffeurValidator : AbstractValidator<FuelCardChauffeur>
    {
        public FuelCardChauffeurValidator()
        {
            RuleFor(s => s.Chaffeur).NotNull().WithMessage("No Chaffeur relation.");
            RuleFor(s => s.FuelCard).NotNull().WithMessage("No FuelCard relation.");
            RuleFor(s => s.IsActive).NotNull().WithMessage("The IsActive property is empty.");
        }
    }
}
