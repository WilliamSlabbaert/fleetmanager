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
    public class UpdateVehicleCommand : IRequest<GenericResult<IGeneralModels>>
    {
        public UpdateVehicleCommand(int vehicleId, Vehicle vehicle)
        {
            this.vehicleId = vehicleId;
            this.vehicle = vehicle;
            this.vehicle.Id = vehicleId;
        }

        public int vehicleId { get; private set; }
        public Vehicle vehicle { get; private set; }
    }
}
