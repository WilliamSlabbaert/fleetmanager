using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class FuelTypeValidator : AbstractValidator<FuelType>
    {
        public FuelTypeValidator()
        {
            RuleFor(c => c.Fuel)
                .NotEmpty().WithMessage("Fuel property is empty.");
        }
    }
}
