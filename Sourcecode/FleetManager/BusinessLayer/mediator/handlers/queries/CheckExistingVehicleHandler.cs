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

namespace BusinessLayer.mediator.handlers.queries
{
    class CheckExistingVehicleHandler : IRequestHandler<CheckExistingVehicleQuery,bool>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IValidator<Vehicle> _validator;
        public CheckExistingVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo,IValidator<Vehicle> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._validator = validator;
        }

        public Task<bool> Handle(CheckExistingVehicleQuery request, CancellationToken cancellationToken)
        {
            if(request.vehicle.Id == 0)
            {
                var results = _vehicleRepo.GetAll(null).FirstOrDefault(s => s.Chassis == request.vehicle.Chassis);
                _validator.Validate(request.vehicle);
                if (results != null)
                {
                    return Task.FromResult(false);
                }
                return Task.FromResult(true); 
            }
            else
            {
                var results = _vehicleRepo.GetAll(null).FirstOrDefault(s => s.Chassis == request.vehicle.Chassis && s.Id != request.vehicle.Id);
                _validator.Validate(request.vehicle);
                if (results != null)
                {
                    return Task.FromResult(false);
                }
                return Task.FromResult(true);
            }
            
        }
    }
}
