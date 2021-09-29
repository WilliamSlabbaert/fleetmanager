using BusinessLayer.models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetLicensePlateFromVehicleQuery : IRequest<LicensePlate>
    {
        public GetLicensePlateFromVehicleQuery(int vehicleId, int licensePlateId)
        {
            this.vehicleId = vehicleId;
            this.licensePlateId = licensePlateId;
        }

        public int vehicleId { get; set; }
        public int licensePlateId { get; set; }
    }
}
