using AutoMapper;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
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

namespace BusinessLayer.mediator.handlers.queries
{
    public class GetVehicleByIdFromChaffeurHandler : IRequestHandler<GetVehicleByIdFromChaffeurQuery, GenericResult<IGeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Vehicle> _validator;
        private IMediator _mediator;
        public GetVehicleByIdFromChaffeurHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator, IMediator mediator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
            this._mediator = mediator;
        }
        public Task<GenericResult<IGeneralModels>> Handle(GetVehicleByIdFromChaffeurQuery request, CancellationToken cancellationToken)
        {
            var vehicle = GetVehicle(request._vehicleId).Result;
            var respond = new GenericResult<IGeneralModels>() { Message = "Vehicle doesn't exist in chaffeurs list."};
            if (vehicle.StatusCode != 200)
            {
                return Task.FromResult(vehicle);
            }
            Vehicle temp = (Vehicle)vehicle.ReturnValue;
            var result = temp.ChaffeurVehicles.FirstOrDefault(s => s.Chaffeur.Id == request._chaffeurId);
            if(result == null)
            {
                respond.SetStatusCode(Overall.ResponseType.NotFound);
                return Task.FromResult(respond);
            }
            respond.SetStatusCode(Overall.ResponseType.OK);
            respond.Message = "Ok";
            respond.ReturnValue = result;
            return Task.FromResult(respond);
        }
        public Task<GenericResult<IGeneralModels>> GetVehicle(int id)
        {
            var vehicle = _mediator.Send(new GetVehicleByIdQuery(id));
            return vehicle;
        }
    }
}
