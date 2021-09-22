using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class AuthenticationValidator : AbstractValidator<AuthenticationType>
    {
        public AuthenticationValidator()
        {
            RuleFor(c => c.type)
                .NotEmpty().WithMessage("Type property is empty.");

        }
    }
}
