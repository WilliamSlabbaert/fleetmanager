using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    class RepairmentValidator : AbstractValidator<Repairment>
    {
        public RepairmentValidator()
        {
            RuleFor(c => c.Date)
                .NotNull().WithMessage("Date property is empty.");

            RuleFor(c => c.Company)
                .NotNull().WithMessage("Company property is empty.")
                .Must(x => x.Length > 0).WithMessage("Company too short.");

            RuleFor(c => c.Description)
                .NotNull().WithMessage("Description property is empty.")
                .Must(x => x.Length > 0).WithMessage("Description too short.");
        }
    }
}
