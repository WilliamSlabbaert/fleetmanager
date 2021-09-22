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
                .NotEmpty().WithMessage("Chassis property is empty.")
                .Must(x => x > 0).WithMessage("Chassis is too short.");

            RuleFor(c => c.Kilometers)
                .NotEmpty().WithMessage("Kilometers property is empty.")
                .Must(x => x > 0).WithMessage("Kilometers is too short.");

            RuleFor(c => c.Type)
                .NotEmpty().WithMessage("Car type property is empty.");

            RuleFor(c => c.FuelType)
                .NotEmpty().WithMessage("Fuel type property is empty.");

            RuleFor(c => c.Brand)
                .NotEmpty().WithMessage("Brand property is empty.")
                .Must(x => x.Length > 0).WithMessage("Brand  is too short.")
                .Must(x => x.Length <= 25).WithMessage("Brand is too long.")
                .Matches(new Regex("^[A-Za-z0-9_-]*$")).WithMessage("Brand cannot contain symbols.");

            RuleFor(c => c.Model)
                .NotEmpty().WithMessage("Model property is empty.")
                .Must(x => x.Length > 0).WithMessage("Model  is too short.")
                .Must(x => x.Length <= 25).WithMessage("Model is too long.")
                .Matches(new Regex("^[A-Za-z0-9_-]*$")).WithMessage("Model cannot contain symbols.");

            RuleFor(c => c.BuildDate)
                .NotEmpty().WithMessage("Build date property is empty.");

        }
    }
}
