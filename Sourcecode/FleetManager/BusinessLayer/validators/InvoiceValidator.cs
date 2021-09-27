using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class InvoiceValidator : AbstractValidator<Invoice>
    {
        public InvoiceValidator()
        {
            RuleFor(c => c.InvoiceImage)
                .NotNull().WithMessage("Invoice image property is empty.");
        }
    }
}
