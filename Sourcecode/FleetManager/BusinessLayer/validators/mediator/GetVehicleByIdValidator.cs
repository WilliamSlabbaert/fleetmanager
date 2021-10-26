using BusinessLayer.mediator.queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators.mediator
{
    public class GetVehicleByIdValidator : AbstractValidator<GetVehicleByIdQuery>
    {
        public GetVehicleByIdValidator()
        {
            RuleFor(s => s)
                .Must(s => s.Id == 0)
                .WithMessage("Id must be greater than 0.")
                .When(s => s != null);

            RuleFor(s => s.Id)
                .NotNull()
                .WithMessage("Id can't be null.");
        }
    }
}
