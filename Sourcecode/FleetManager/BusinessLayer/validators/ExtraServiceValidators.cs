using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class ExtraServiceValidators : AbstractValidator<ExtraService>
    {
        public ExtraServiceValidators()
        {
            RuleFor(c => c.Service)
                .NotEmpty().WithMessage("Service property is empty.");
        }
    }
}
