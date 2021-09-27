using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class LicensePlateValidator : AbstractValidator<LicensePlate>
    {
        public LicensePlateValidator()
        {
            RuleFor(c => c.Plate)
                .NotNull().WithMessage("Plate property is empty.");
        }
    }
}
