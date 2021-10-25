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
    public class GetLicensePlateByIdQuery : IRequest<GenericResult<GeneralModels>>
    {
        public GetLicensePlateByIdQuery(int licensePlateId)
        {
            this.licensePlateId = licensePlateId;
        }

        public int licensePlateId { get; private set; }
    }
}
