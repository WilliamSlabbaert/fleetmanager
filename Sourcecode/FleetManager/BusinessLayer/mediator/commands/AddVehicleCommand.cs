using BusinessLayer.models;
using BusinessLayer.validators.response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.commands
{
    public class AddVehicleCommand : IRequest<Vehicle>
    {
        public Vehicle _vehicle { get; private set; }
        public List<GenericResponse> _errors { get; set; }

        public AddVehicleCommand(Vehicle vehicle)
        {
            this._vehicle = vehicle;
            this._errors = new List<GenericResponse>();
        }
    }
}
