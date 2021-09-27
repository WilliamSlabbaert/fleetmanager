using BusinessLayer.models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetVehicleByIdQuery : IRequest<Vehicle>
    {
        public int Id { get; private set; }

        public GetVehicleByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}
