using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators;
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
        private IValidator<Vehicle> _vehicleValidator;

        public AddVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IMediator mediator,
            VehicleValidator vehicleV)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._mediator = mediator;
            this._vehicleValidator = vehicleV;
        }

        public Task<GenericResult<GeneralModels>> Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = _mapper.Map<Vehicle>(request._vehicle);
            var check = _vehicleValidator.Validate(vehicle);
            var resultCheck = GenericValidationCheck.CheckModel(check, "Vehicle is invalid.");
            if (check.IsValid)
            {
                var temp = _mapper.Map<VehicleEntity>(vehicle);
                var result = _mediator.Send(new CheckExistingVehicleQuery(vehicle));
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
            return Task.FromResult(resultCheck);
        }
    }
}
