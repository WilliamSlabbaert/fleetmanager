using BusinessLayer.models;
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
    public class GetVehicleByIdQuery : IRequest<GenericResult<GeneralModels>>
    {
        public int Id { get; private set; }

        public GetVehicleByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
