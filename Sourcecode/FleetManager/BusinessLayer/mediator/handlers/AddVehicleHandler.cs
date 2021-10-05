using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
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
    public class AddVehicleHandler : IRequestHandler<AddVehicleCommand, Vehicle>
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
        Task<Vehicle> IRequestHandler<AddVehicleCommand, Vehicle>.Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var temp = _mapper.Map<VehicleEntity>(request._vehicle);
            var result = _mediator.Send(new CheckExistingVehicleQuery(request._vehicle));
            if (result.Result == false)
            {
                //return Task.FromResult(result: new GenericResponse());
            }
            _vehicleRepo.AddEntity(temp);
            _vehicleRepo.Save();
            return Task.FromResult(_mapper.Map<Vehicle>(temp));
        }
    }
}
