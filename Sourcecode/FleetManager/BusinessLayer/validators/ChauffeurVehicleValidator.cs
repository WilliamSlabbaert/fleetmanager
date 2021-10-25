using BusinessLayer.models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.validators
{
    public class ChauffeurVehicleValidator : AbstractValidator<VehicleChauffeur>
    {
        public ChauffeurVehicleValidator()
        {
            RuleFor(s => s.Chauffeur).NotNull().WithMessage("No Chaffeur relation.");
            RuleFor(s => s.Vehicle).NotNull().WithMessage("No Vehicle relation.");
            RuleFor(s => s.IsActive).NotNull().WithMessage("The IsActive property is empty.");
        }
    }
}
