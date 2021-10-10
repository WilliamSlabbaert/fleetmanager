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
    public class GetVehiclesPagingHandler : IRequestHandler<GetVehiclesPagingQuery, GenericResult>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Vehicle> _validator;
        private IMediator _mediator;
        public GetVehiclesPagingHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator, IMediator mediator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
            this._mediator = mediator;
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
        public Task<GenericResult> Handle(GetVehiclesPagingQuery request, CancellationToken cancellationToken)
        {
            var temp = Task.FromResult(_mapper.Map<List<Vehicle>>(_vehicleRepo.GetAllWithPaging(
                s => s.Include(s => s.ChaffeurVehicles)
                .ThenInclude(s => s.Chaffeur)
                .Include(s => s.LicensePlates)
                .Include(s => s.Requests),request._parameters).ToList())).Result;

            var value = temp == null ? null : _mapper.Map<List<Vehicle>>(temp);
            var result = CreateResult(temp == null, value);
            return Task.FromResult(result);
        }
    }
}
