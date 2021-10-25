using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.entities.generic;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetVehicleLicensePlatesQuery :  IRequest<GenericResult<GeneralModels>>
    {
        public int Id { get; private set; }

        public GetVehicleLicensePlatesQuery(int id)
        {
            this.Id = id;
        }
    }
}
