using BusinessLayer.validators.response;
using DataLayer.entities.paging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetVehiclesPagingQuery : IRequest<GenericResult>
    {
        public GetVehiclesPagingQuery(GenericParemeters parameters)
        {
            _parameters = parameters;
        }

        public GenericParemeters _parameters { get; private set; }
    }
}
