using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetVehiclesPagingQuery : IRequest<GenericResult<GeneralModels>>
    {
        public GetVehiclesPagingQuery(GenericParameter parameters)
        {
            _parameters = parameters;
        }

        public GenericParameter _parameters { get; private set; }
    }
}
