using BusinessLayer.models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class CheckExistingLicensePlateQuery : IRequest<bool>
    {
        public CheckExistingLicensePlateQuery(LicensePlate licensePlate)
        {
            this.licensePlate = licensePlate;
        }

        public LicensePlate licensePlate { get; private set; }
    }
}
