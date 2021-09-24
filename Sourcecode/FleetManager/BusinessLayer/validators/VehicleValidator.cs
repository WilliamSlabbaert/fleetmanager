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

            RuleFor(c => c.Kilometers)
                .NotEmpty().WithMessage("Kilometers property is empty.");

            RuleFor(c => c.Type)
                .NotNull().WithMessage("Car type property is null.");

            RuleFor(c => c.Type)
                .IsInEnum().WithMessage("Type should be enum.")
                .When(s => s.Type != null); 

            RuleFor(c => c.FuelType)
                .NotNull().WithMessage("Fuel type property is null.");

            RuleFor(c => c.FuelType)
                .IsInEnum().WithMessage("FuelType should be enum.")
                .When(s => s.FuelType != null);

            RuleFor(c => c.Brand)
                .NotEmpty().WithMessage("Brand property is empty.");

            RuleFor(c => c.Model)
                .NotEmpty().WithMessage("Model property is empty.");

            RuleFor(c => c.BuildDate)
                .NotEmpty().WithMessage("Build date property is empty.");

        }
    }
}
