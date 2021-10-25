using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using DataLayer.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetVehiclesQuery : IRequest<GenericResult<GeneralModels>>
    {

    }
}
