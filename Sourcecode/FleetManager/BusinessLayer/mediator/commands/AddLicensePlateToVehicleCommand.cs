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
    public class AddLicensePlateToVehicleCommand : IRequest
    {
        public AddLicensePlateToVehicleCommand(int vehicleId, LicensePlate licensePlate)
        {
            this.vehicleId = vehicleId;
            this.licensePlate = licensePlate;
            this._errors = new();
        }

        public int vehicleId { get; private set; }
        public List<GenericResponse> _errors { get; set; }
        public LicensePlate licensePlate { get; private set; }

    }
}
