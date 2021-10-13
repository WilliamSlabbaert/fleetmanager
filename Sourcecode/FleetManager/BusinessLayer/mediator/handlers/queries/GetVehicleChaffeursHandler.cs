using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
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

namespace BusinessLayer.mediator.handlers.queries
{
    public class GetVehicleChaffeursHandler : IRequestHandler<GetVehicleChaffeursQuery, GenericResult<IGeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private IMediator _mediator;
        public GetVehicleChaffeursHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IMediator mediator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._mediator = mediator;
        }
        public Task<GenericResult<IGeneralModels>> Handle(GetVehicleChaffeursQuery request, CancellationToken cancellationToken)
        {
            var vehicles = _vehicleRepo.GetAll(s => s.Include(x => x.ChaffeurVehicles).ThenInclude(s=>s.Chaffeur));
            var temp = vehicles.FirstOrDefault(s => s.Id == request.Id);

            var value = temp == null ? null : _mapper.Map<Vehicle>(temp).ChaffeurVehicles;
            var result = CreateResult(temp == null, value);
            return Task.FromResult(result);
        }
        public GenericResult<IGeneralModels> CreateResult(bool check, object value)
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
