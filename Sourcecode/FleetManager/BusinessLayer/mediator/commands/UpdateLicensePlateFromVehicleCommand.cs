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
    public class UpdateLicensePlateFromVehicleCommand : IRequest<GenericResult<IGeneralModels>>
    {

        public UpdateLicensePlateFromVehicleCommand(int vehicleId,int licensePlateId , LicensePlateDTO licensePlate)
        {
            _vehicleId = vehicleId;
            _licensePlateId = licensePlateId;
            _licensePlate = licensePlate;
        }

        public int _vehicleId { get; set; }
        public int _licensePlateId { get; set; }
        public LicensePlateDTO _licensePlate { get; set; }
    }
}
