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
    public class UpdateVehicleCommand : IRequest<GenericResult<GeneralModels>>
    {
        public UpdateVehicleCommand(int vehicleId, VehicleDTO vehicle)
        {
            this.vehicleId = vehicleId;
            this.vehicle = vehicle;
        }

        public int vehicleId { get; private set; }
        public VehicleDTO vehicle { get; private set; }
    }
}
