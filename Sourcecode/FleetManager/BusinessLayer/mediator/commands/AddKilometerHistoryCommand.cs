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
    public class AddKilometerHistoryCommand : IRequest<GenericResult<IGeneralModels>>
    {
        public AddKilometerHistoryCommand(int vehicleId, KilometerHistory kilometer)
        {
            _vehicleId = vehicleId;
            this.kilometer = kilometer;
        }

        public int _vehicleId { get; private set; }
        public KilometerHistory kilometer { get; private set; }
    }
}
