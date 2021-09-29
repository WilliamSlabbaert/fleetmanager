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
    public class UpdateLicensePlateFromVehicleCommand : IRequest<LicensePlate>
    {

        public UpdateLicensePlateFromVehicleCommand(int vehicleId,int licensePlateId , LicensePlate licensePlate)
        {
            _vehicleId = vehicleId;
            _licensePlateId = licensePlateId;
            _licensePlate = licensePlate;
            _errors = new();
        }

        public int _vehicleId { get; set; }
        public int _licensePlateId { get; set; }
        public LicensePlate _licensePlate { get; set; }
        public List<GenericResponse> _errors { get; set; }
    }
}
