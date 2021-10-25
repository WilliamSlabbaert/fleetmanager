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
    public class GetVehicleByIdFromChauffeurQuery : IRequest<GenericResult<IGeneralModels>>
    {
        public GetVehicleByIdFromChauffeurQuery(int chauffeurId, int vehicleId)
        {
            _chauffeurId = chauffeurId;
            _vehicleId = vehicleId;
        }

        public int _chauffeurId { get; private set; }
        public int _vehicleId { get; private set; }
    }
}
