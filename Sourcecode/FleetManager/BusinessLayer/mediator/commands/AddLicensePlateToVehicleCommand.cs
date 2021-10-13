﻿using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.commands
{
    public class AddLicensePlateToVehicleCommand : IRequest<GenericResult<IGeneralModels>>
    {
        public AddLicensePlateToVehicleCommand(int vehicleId, LicensePlate licensePlate)
        {
            this.vehicleId = vehicleId;
            this.licensePlate = licensePlate;
        }

        public int vehicleId { get; private set; }
        public LicensePlate licensePlate { get; private set; }

    }
}
