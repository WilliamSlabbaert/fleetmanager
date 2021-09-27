using BusinessLayer.models;
using DataLayer.entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetVehiclesQuery : IRequest<List<Vehicle>>
    {

    }
}
