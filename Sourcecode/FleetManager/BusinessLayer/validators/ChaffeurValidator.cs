using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class ChaffeurValidator : AbstractValidator<Chauffeur>
    {
        public ChaffeurValidator()
        {
            RuleFor(c => c.City)
                .NotNull().WithMessage("City property is null.");

            When(x => string.IsNullOrEmpty(x.City) == false, () =>
            {
                RuleFor(c => c.City)
                .Must(x => x.Length > 0).WithMessage("City is too short.")
                .Must(x => x.Length <= 25).WithMessage("City is too long.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("City cannot contain symbols and numbers.")
                .When(s => s != null);
            });

            RuleFor(c => c.Street)
                .NotNull().WithMessage("Street property is null.");
            
            When(x => string.IsNullOrEmpty(x.Street) == false, () =>
            {
                RuleFor(c => c.Street)
                .Must(x => x.Length > 0).WithMessage("Street is too short.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("Street cannot contain symbols and numbers.")
                .When(s => s != null);
            });

            RuleFor(c => c.HouseNumber)
                .NotNull().WithMessage("House number property is null.");

            When(x => string.IsNullOrEmpty(x.HouseNumber) == false, () =>
            {
                RuleFor(c => c.HouseNumber)
                .Must(x => x.Length > 0).WithMessage("House number is too short.")
                .Matches(new Regex("^[1-9]\\d*(?:[ -]?(?:[a-zA-Z]+|[1-9]\\d*))?$")).WithMessage("House number must contain at least 1 number and 1 letter or contain only a number.")
                .When(s => s != null);
            });
            

            RuleFor(c => c.FirstName)
                .NotNull().WithMessage("First name property is null.");

            When(x => string.IsNullOrEmpty(x.FirstName) == false, () =>
            {
                RuleFor(c => c.FirstName)
               .Must(x => x.Length > 0).WithMessage("First name is too short.")
               .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("First name cannot contain symbols and numbers.")
               .When(s => s != null);
            });
           
            RuleFor(c => c.LastName)
                .NotNull().WithMessage("Last name property is null.");
            When(x => string.IsNullOrEmpty(x.LastName) == false, () =>
            {
                RuleFor(c => c.LastName)
                .Must(x => x.Length > 0).WithMessage("Last name is too short.")
                .Matches(new Regex("^[[A-Za-z]]*((-|\\s)*[A-Za-z])*$")).WithMessage("Last name cannot contain symbols and numbers.")
                .When(s => s != null);
            });

            RuleFor(c => c.DateOfBirth)
                .NotNull().WithMessage("Date of birth is null.");

            RuleFor(c => c.DateOfBirth)
                .Must(x => x < DateTime.Now).WithMessage("Date of birth is must be earlier then today.")
                .When(s => s != null);

            RuleFor(c => c.NationalInsurenceNumber)
            .NotNull().WithMessage("National insurence number is null.");


            When(x => string.IsNullOrEmpty(x.NationalInsurenceNumber) == false, () =>
           {
               RuleFor(c => c.NationalInsurenceNumber)
               .Must((s, x) => x.Length > 0).WithMessage("National insurence number is too short.")
               .Must((s, x) => CheckYear(s)).WithMessage("National insurence number year(number) is wrong.")
               .Must((s, x) => CheckMonth(s)).WithMessage("National insurence number month(number) is wrong.")
               .Must((s, x) => CheckDay(s)).WithMessage("National insurence number day(number) is wrong.")
               .Matches(new Regex("^\\d([0-9]|1[0-9])\\.(0[0-9]|1[012])\\.(0[1-9]|[12][0-9]|3[01])\\-([0-9][0-9][0-9])\\.([0-9][0-9])$")).WithMessage("National insurence number is invalid structure. (yy.mm.dd-xxx.xx)");
           });

        }
        protected bool CheckYear(object input)
        {
            try
            {
                var chaffeur = (Chauffeur)input;
                var splitValue = chaffeur.NationalInsurenceNumber.Split(".");
                var year = chaffeur.DateOfBirth.Year.ToString();
                if (year.Substring(year.Length - 2) == splitValue[0])
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
        protected bool CheckMonth(object input)
        {
            try
            {
                var chaffeur = (Chauffeur)input;
                var splitValue = chaffeur.NationalInsurenceNumber.Split(".");
                var month = chaffeur.DateOfBirth.Month.ToString();
                if (month.Length == 1)
                {
                    month = "0" + month;
                }
                if (month == splitValue[1])
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
        protected bool CheckDay(object input)
        {
            try
            {
                var chaffeur = (Chauffeur)input;
                var splitValue = chaffeur.NationalInsurenceNumber.Split(".");
                var day = chaffeur.DateOfBirth.Day.ToString();
                if (day.Length == 1)
                {
                    day = "0" + day;
                }
                var checkDay = splitValue[2].Substring(0, 2);
                if (day == checkDay)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }

        }
    }
}
