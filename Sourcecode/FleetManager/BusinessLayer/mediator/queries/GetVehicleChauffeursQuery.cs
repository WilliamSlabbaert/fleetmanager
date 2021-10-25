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
    public class GetVehicleChaffeursQuery : IRequest<GenericResult<IGeneralModels>>
    {
        public int Id { get; private set; }

        public GetVehicleChaffeursQuery(int id)
        {
            this.Id = id;
        }
    }
}
