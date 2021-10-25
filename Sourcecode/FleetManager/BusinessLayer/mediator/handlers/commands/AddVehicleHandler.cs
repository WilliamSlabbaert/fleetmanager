using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.handlers
{
    public class AddVehicleHandler : IRequestHandler<AddVehicleCommand, GenericResult<GeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public AddVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IMediator mediator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._mediator = mediator;
        }

        public Task<GenericResult<GeneralModels>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var t = _mapper.Map<Vehicle>(request._vehicle);
            var temp = _mapper.Map<VehicleEntity>(t);
            var result = _mediator.Send(new CheckExistingVehicleQuery(t));
            var respond = new GenericResult<GeneralModels>() { Message = "Vehicle with same chasis number already exists." };
            respond.SetStatusCode(Overall.ResponseType.BadRequest);
            if (result.Result == false)
            {
                return Task.FromResult(respond);
            }
            _vehicleRepo.AddEntity(temp);
            _vehicleRepo.Save();
            respond.SetStatusCode(Overall.ResponseType.OK);
            respond.Message = "Ok";
            respond.ReturnValue = _mapper.Map<Vehicle>(temp);
            return Task.FromResult(respond);
        }
    }
}
