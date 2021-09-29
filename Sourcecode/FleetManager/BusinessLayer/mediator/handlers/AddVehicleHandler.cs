using AutoMapper;
using BusinessLayer.mediator.commands;
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
        private readonly IValidator<Vehicle> _validator;
        public AddVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
        }
        Task<Vehicle> IRequestHandler<AddVehicleCommand, Vehicle>.Handle(AddVehicleCommand request, CancellationToken cancellationToken)
        {
            var results = _validator.Validate(request._vehicle);
            var temp = _mapper.Map<VehicleEntity>(request._vehicle);
            if (results.IsValid == false)
            {
                request._errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                _vehicleRepo.AddEntity(temp);
                _vehicleRepo.Save();
            }
            return Task.FromResult(_mapper.Map<Vehicle>(temp));
        }
    }
}
