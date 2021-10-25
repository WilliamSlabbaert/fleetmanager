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
    public class DeleteKilometerHistoryCommand : IRequest<GenericResult<GeneralModels>>
    {
        public DeleteKilometerHistoryCommand(int kilometerId, int vehicleId)
        {
            _kilometerId = kilometerId;
            _vehicleId = vehicleId;
        }

        public int _kilometerId { get; private set; }
        public int _vehicleId { get; private set; }
    }
}
