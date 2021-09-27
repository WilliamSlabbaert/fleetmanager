using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class ExtraServiceValidator : AbstractValidator<ExtraService>
    {
        public ExtraServiceValidator()
        {
            RuleFor(c => c.Service)
                .NotNull().WithMessage("Service property is empty.");
        }
    }
}
