using BusinessLayer.models;
using BusinessLayer.models.general;
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
        public Vehicle _vehicle { get; private set; }

        public AddVehicleCommand(Vehicle vehicle)
        {
            this._vehicle = vehicle;
        }
    }
}
