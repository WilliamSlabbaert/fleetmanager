using AutoMapper;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
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
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, GenericResult>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Vehicle> _validator;
        public GetVehicleByIdHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
        }
        Task<GenericResult> IRequestHandler<GetVehicleByIdQuery, GenericResult>.Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var temp = Task.FromResult(_mapper.Map<Vehicle>(
                _vehicleRepo.GetById(
                    filter: s => s.Id == request.Id,
                    s => s.Include(s => s.Requests)
                    .Include(s => s.ChaffeurVehicles)
                    .ThenInclude(s => s.Chaffeur)
                    .Include(s => s.LicensePlates))
                ));
            var result = new GenericResult();
            if (temp == null)
            {
                result.Message = "Vehicle is not found.";
                result.SetStatusCode(Overall.ResponseType.NotFound);
                return Task.FromResult(result);
            }
            result.Message = "Vehicle is found.";
            result.SetStatusCode(Overall.ResponseType.OK);
            result.ReturnValue = temp.Result;
            return Task.FromResult(result);
        }
    }
}
