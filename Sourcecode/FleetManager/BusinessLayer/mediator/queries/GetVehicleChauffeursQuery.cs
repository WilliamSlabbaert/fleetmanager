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
    public class GetVehicleChauffeursQuery : IRequest<GenericResult<GeneralModels>>
    {
        public int Id { get; private set; }

        public GetVehicleChauffeursQuery(int id)
        {
            this.Id = id;
        }
    }
}
