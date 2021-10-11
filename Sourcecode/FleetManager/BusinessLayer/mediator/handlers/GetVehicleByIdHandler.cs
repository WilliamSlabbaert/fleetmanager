using AutoMapper;
using BusinessLayer.mediator.commands;
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
        private IMediator _mediator;
        public GetVehicleByIdHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator, IMediator mediator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
            this._mediator = mediator;
        }
        Task<GenericResult> IRequestHandler<GetVehicleByIdQuery, GenericResult>.Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            var temp = _mapper.Map<Vehicle>(
                _vehicleRepo.GetById(
                    filter: s => s.Id == request.Id,
                    s => s.Include(s => s.Requests)
                    .Include(s => s.ChaffeurVehicles)
                    .ThenInclude(s => s.Chaffeur)
                    .Include(s => s.LicensePlates)
                ));
            var value = temp == null ? null : temp;
            var result = CreateResult(temp == null, value);

            return Task.FromResult(result);
        }
        public GenericResult CreateResult(bool check, object value)
        {
            var message = "OK";
            var code = Overall.ResponseType.OK;
            if (check)
            {
                message = "Vehicle('s) not found";
                code = Overall.ResponseType.NotFound;
                value = null;
            }
            var resp = _mediator.Send(new CreateGenericResultCommand(message, code, value));
            return resp.Result;
        }
    }
}
