using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class FuelCardChaffeurValidator : AbstractValidator<FuelCardChaffeur>
    {
        public FuelCardChaffeurValidator()
        {
            RuleFor(s => s.Chaffeur).NotEmpty().WithMessage("No Chaffeur relation.");
            RuleFor(s => s.FuelCard).NotEmpty().WithMessage("No FuelCard relation.");
            RuleFor(s => s.IsActive).NotEmpty().WithMessage("The IsActive property is empty.");
        }
    }
}
