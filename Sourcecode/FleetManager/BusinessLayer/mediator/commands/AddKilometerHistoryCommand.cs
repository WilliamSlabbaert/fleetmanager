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
    public class AddKilometerHistoryCommand : IRequest<GenericResult<GeneralModels>>
    {
        public AddKilometerHistoryCommand(int vehicleId, KilometerHistoryDTO kilometer)
        {
            _vehicleId = vehicleId;
            this.kilometer = kilometer;
        }

        public int _vehicleId { get; private set; }
        public KilometerHistoryDTO kilometer { get; private set; }
    }
}
