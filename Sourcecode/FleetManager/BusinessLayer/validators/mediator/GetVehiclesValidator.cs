using BusinessLayer.mediator.queries;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators.mediator
{
    public class GetVehiclesValidator : AbstractValidator<GetVehiclesPagingQuery>
    {
        public GetVehiclesValidator()
        {
            RuleFor(s => s._parameters)
                .NotNull()
                .WithMessage("Parameters can't be null.");

            RuleFor(s => s)
                .Must(s=> s._parameters.PageNumber > 0)
                .WithMessage("Pagenumber can't be 0.")
                .When(s=> s != null);

            RuleFor(s => s)
                .Must(s => s._parameters.PageSize > 0)
                .WithMessage("Pagesize can't be 0.")
                .When(s => s != null);
        }
    }
}
