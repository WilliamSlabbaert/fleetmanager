using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetVehicleByIdFromChaffeurQuery : IRequest<GenericResult<IGeneralModels>>
    {
        public GetVehicleByIdFromChaffeurQuery(int chaffeurId, int vehicleId)
        {
            _chaffeurId = chaffeurId;
            _vehicleId = vehicleId;
        }

        public int _chaffeurId { get; private set; }
        public int _vehicleId { get; private set; }
    }
}
