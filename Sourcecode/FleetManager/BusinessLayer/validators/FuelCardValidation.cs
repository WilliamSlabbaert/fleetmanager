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
    public class FuelCardValidation : AbstractValidator<FuelCard>
    {
        public FuelCardValidation()
        {
            RuleFor(c => c.CardNumber)
                .NotEmpty().WithMessage("Card number  property is empty.")
                .Must(x => x.Length > 0).WithMessage("Card number is too short.")
                .Matches(new Regex("^[a-zA-Z0-9]*$")).WithMessage("Card number cannot contain symbols.");

            RuleFor(c => c.IsActive)
                .NotEmpty().WithMessage("Is Active property is empty.");

            RuleFor(c => c.Pin)
                .NotEmpty().WithMessage("Pin property is empty.")
                .Must(x => x.Length > 0).WithMessage("Pin is too short.")
                .Must(x => x.Length <= 4).WithMessage("Pin too short.")
                .Matches(new Regex("^[0-9]*$")).WithMessage("Pin cannot contain symbols and letters.");

            RuleFor(c => c.CardNumber)
                .NotEmpty().WithMessage("Card number  property is empty.")
                .Must(x => x.Length > 0).WithMessage("Card number is too short.")
                .Matches(new Regex("^[0-9]*$")).WithMessage("Card number cannot contain symbols and letters.");
        }
    }
}
