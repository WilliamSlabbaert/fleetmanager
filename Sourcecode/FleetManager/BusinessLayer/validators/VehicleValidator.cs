using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(c => c.Chassis)
                .NotEmpty().WithMessage("Chassis property is empty.");

            RuleFor(c => c.Type)
                .IsInEnum().WithMessage("Type should be enum."); 

            RuleFor(c => c.FuelType)
                .IsInEnum().WithMessage("FuelType should be enum.");

            RuleFor(c => c.Brand)
                .NotEmpty().WithMessage("Brand property is empty.");

            RuleFor(c => c.Model)
                .NotEmpty().WithMessage("Model property is empty.");

            RuleFor(c => c.BuildDate)
                .NotEmpty().WithMessage("Build date property is empty.");

        }
    }
}
