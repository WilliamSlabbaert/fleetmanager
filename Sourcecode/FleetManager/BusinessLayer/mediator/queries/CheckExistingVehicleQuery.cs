using BusinessLayer.models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class CheckExistingVehicleQuery : IRequest<bool>
    {
        public CheckExistingVehicleQuery(Vehicle vehicle)
        {
            this.vehicle = vehicle;
        }

        public Vehicle vehicle { get; private set; }
    }
}
