﻿using BusinessLayer.validators.response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.queries
{
    public class GetVehicleRequestsQuery : IRequest<GenericResult>
    {
        public int Id { get; private set; }

        public GetVehicleRequestsQuery(int id)
        {
            this.Id = id;
        }
    }
}