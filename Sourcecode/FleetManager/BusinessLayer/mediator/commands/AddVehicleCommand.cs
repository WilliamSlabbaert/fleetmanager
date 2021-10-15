using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.models.input;
using BusinessLayer.validators.response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.commands
{
    public class AddVehicleCommand : IRequest<GenericResult<IGeneralModels>>
    {
        public VehicleDTO _vehicle { get; private set; }

        public AddVehicleCommand(VehicleDTO vehicle)
        {
            this._vehicle = vehicle;
        }
    }
}
