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
                .NotNull().WithMessage("City property is null.")
                .Must(x => x.Length > 0).WithMessage("City is too short.")
                .Must(x => x.Length <= 25).WithMessage("City is too long.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("City cannot contain symbols and numbers.");

            RuleFor(c => c.Street)
                .NotNull().WithMessage("Street property is null.")
                .Must(x => x.Length > 0).WithMessage("Street is too short.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("Street cannot contain symbols and numbers.");

            RuleFor(c => c.HouseNumber)
                .NotNull().WithMessage("House number property is null.")
                .Must(x => x.Length > 0).WithMessage("House number is too short.")
                .Matches(new Regex("^[1-9]\\d*(?:[ -]?(?:[a-zA-Z]+|[1-9]\\d*))?$")).WithMessage("House number must contain at least 1 number and 1 letter or contain only a number.");

            RuleFor(c => c.FirstName)
                .NotNull().WithMessage("First name property is null.")
                .Must(x => x.Length > 0).WithMessage("First name is too short.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("First name cannot contain symbols and numbers.");

            RuleFor(c => c.LastName)
                .NotNull().WithMessage("Last name property is null.")
                .Must(x => x.Length > 0).WithMessage("Last name is too short.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("Last name cannot contain symbols and numbers.");

            RuleFor(c => c.DateOfBirth)
                .NotNull().WithMessage("Date of birth is null.")
                .Must(x => x < DateTime.Now).WithMessage("Date of birth is must be earlier then today.");

            RuleFor(c => c.NationalInsurenceNumber)
                .NotNull().WithMessage("National insurence number is null.")
                .Must(x => x.Length > 0).WithMessage("National insurence number is too short.");

            RuleFor(c => c.IsActive)
                .NotNull().WithMessage("Is Active insurence number is null."); //onnodig
        }
    }
}
