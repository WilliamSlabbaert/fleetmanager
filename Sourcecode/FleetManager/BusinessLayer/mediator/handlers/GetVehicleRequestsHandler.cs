using AutoMapper;
using BusinessLayer.mediator.queries;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.handlers
{
    public class GetVehicleRequestsHandler : IRequestHandler<GetVehicleRequestsQuery, GenericResult>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        public GetVehicleRequestsHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
        }
        public Task<GenericResult> Handle(GetVehicleRequestsQuery request, CancellationToken cancellationToken)
        {
            var vehicles = _vehicleRepo.GetAll(s => s.Include(x => x.Requests));
            var temp = vehicles.FirstOrDefault(s => s.Id == request.Id);

            var result = new GenericResult();
            if (temp == null)
            {
                result.Message = "Vehicle is not found.";
                result.SetStatusCode(Overall.ResponseType.NotFound);
                return Task.FromResult(result);
            }
            result.Message = "Ok";
            result.SetStatusCode(Overall.ResponseType.OK);
            result.ReturnValue = temp.Requests;
            return Task.FromResult(result);
        }
    }
}
