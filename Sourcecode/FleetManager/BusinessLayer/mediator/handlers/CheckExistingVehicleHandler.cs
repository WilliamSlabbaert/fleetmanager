using AutoMapper;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
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
    class CheckExistingVehicleHandler : IRequestHandler<CheckExistingVehicleQuery, bool>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Vehicle> _validator;
        public CheckExistingVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
        }
        public Task<bool> Handle(CheckExistingVehicleQuery request, CancellationToken cancellationToken)
        {
            var results = _vehicleRepo.GetAll(null).FirstOrDefault(s => s.Chassis == request.vehicle.Chassis);
            if(results == null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}
