using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class RequestValidator : AbstractValidator<Request>
    {
        public RequestValidator()
        {
            RuleFor(c => c.StartDate)
                .NotEmpty().WithMessage("Start date property is empty.");

            RuleFor(x => x)
                .Must(x => x.StartDate < x.EndDate).WithMessage("Start date should be earlier then end date.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status property is empty.")
                .Must(x => x.Length > 0).WithMessage("Status is too short.");
                
        }
    }
}
