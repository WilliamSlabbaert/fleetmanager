using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class KilometerHistoryValidator : AbstractValidator<KilometerHistory>
    {
        public KilometerHistoryValidator()
        {
            RuleFor(c => c.Date)
                .NotNull().WithMessage("Date property is empty.");

            RuleFor(c => c.Kilometers)
                .NotNull().WithMessage("Date property is empty.");

            RuleFor(c => c.Kilometers)
                .Must(c => c >= 0).WithMessage("Kilometers property can't be lower then 0.");

        }
    }
}
