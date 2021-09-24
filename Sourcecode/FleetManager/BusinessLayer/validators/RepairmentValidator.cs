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
                .NotEmpty().WithMessage("Date property is empty.");

            RuleFor(c => c.Company)
                .NotEmpty().WithMessage("Company property is empty.")
                .Must(x => x.Length > 0).WithMessage("Company too short.");

            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Description property is empty.")
                .Must(x => x.Length > 0).WithMessage("Description too short.");
        }
    }
}
