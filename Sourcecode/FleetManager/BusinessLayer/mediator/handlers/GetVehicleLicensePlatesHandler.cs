using AutoMapper;
using BusinessLayer.mediator.queries;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
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
    public class GetVehicleLicensePlatesHandler : IRequestHandler<GetVehicleLicensePlatesQuery, GenericResult>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        public GetVehicleLicensePlatesHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
        }
        public Task<GenericResult> Handle(GetVehicleLicensePlatesQuery request, CancellationToken cancellationToken)
        {
            var vehicles = _vehicleRepo.GetAll(s => s.Include(x => x.LicensePlates));
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
            result.ReturnValue = temp.LicensePlates;
            return Task.FromResult(result);
        }
    }
}
