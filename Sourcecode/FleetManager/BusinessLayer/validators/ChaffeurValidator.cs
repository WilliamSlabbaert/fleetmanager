using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class ChaffeurValidator : AbstractValidator<Chaffeur>
    {
        public ChaffeurValidator()
        {
            RuleFor(c => c.City)
                .NotEmpty().WithMessage("City property is empty.")
                .Must(x => x.Length > 0).WithMessage("City is too short.")
                .Must(x => x.Length <= 25).WithMessage("City is too long.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("City cannot contain symbols and numbers.");

            RuleFor(c => c.Street)
                .NotEmpty().WithMessage("Street property is empty.")
                .Must(x => x.Length > 0).WithMessage("Street is too short.")
                .Must(x => x.Length <= 25).WithMessage("Street is too long.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("Street cannot contain symbols and numbers.");

            RuleFor(c => c.HouseNumber)
                .NotEmpty().WithMessage("House number property is empty.")
                .Must(x => x.Length > 0).WithMessage("House number  is too short.")
                .Must(x => x.Length <= 25).WithMessage("House number  is too long.")
                .Matches(new Regex("^[1-9]\\d*(?:[ -]?(?:[a-zA-Z]+|[1-9]\\d*))?$")).WithMessage("House number must contain at least 1 number and 1 letter or contain only a number.");

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("First name property is empty.")
                .Must(x => x.Length > 0).WithMessage("First name is too short.")
                .Must(x => x.Length <= 25).WithMessage("First name is too long.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("First name cannot contain symbols and numbers.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Last name property is empty.")
                .Must(x => x.Length > 0).WithMessage("Last name is too short.")
                .Must(x => x.Length <= 25).WithMessage("Last name is too long.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("Last name cannot contain symbols and numbers.");

            RuleFor(c => c.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is empty.")
                .Must(x => x < DateTime.Now).WithMessage("Date of birth is must be earlier then today.");

            RuleFor(c => c.NationalInsurenceNumber)
                .NotEmpty().WithMessage("National insurence number is empty.");

            RuleFor(c => c.IsActive)
                .NotEmpty().WithMessage("Is Active insurence number is empty.");
        }
    }
}
