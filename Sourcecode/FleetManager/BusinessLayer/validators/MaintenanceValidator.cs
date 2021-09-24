using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    class MaintenanceValidator : AbstractValidator<Maintenance>
    {
        public MaintenanceValidator()
        {
            RuleFor(c => c.Date)
                .NotEmpty().WithMessage("Date property is empty.");

            RuleFor(c => c.Price)
                .NotEmpty().WithMessage("Price property is empty.")
                .Must(x => x > 0).WithMessage("Price cannot be lower then 0.");

            RuleFor(c => c.Garage)
                .NotEmpty().WithMessage("Garage property is empty.")
                .Must(x => x.Length > 0).WithMessage("Garage too short.");
        }
    }
}
