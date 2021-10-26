using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class FuelCardValidator : AbstractValidator<FuelCard>
    {
        public FuelCardValidator()
        {
            RuleFor(c => c.CardNumber)
                .NotNull().WithMessage("Card number property is empty.");

            RuleFor(c => c.CardNumber)
                .Must(x => x.Length > 0).WithMessage("Card number is too short.")
                .Matches(new Regex("^[a-zA-Z0-9]*$")).WithMessage("Card number cannot contain symbols.")
                .When(x => x != null);

            RuleFor(c => c.IsActive)
                .NotNull().WithMessage("Is Active property is empty.");

            RuleFor(c => c.Pin)
                .NotNull().WithMessage("Pin property is empty.");

            RuleFor(c => c.Pin)
                .Must(x => x.Length > 0).WithMessage("Pin is too short.")
                .Must(x => x.Length <= 4).WithMessage("Pin too long.")
                .Matches(new Regex("^[0-9]*$")).WithMessage("Pin cannot contain symbols and letters.")
                .When(x => x != null);

            RuleFor(c => c.ValidityDate)
                .NotNull().WithMessage("Date property is empty.");
        }
    }
}
