using AutoMapper;
using BusinessLayer.mediator.commands;
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
    public class UpdateLicensePlateFromVehicleHandler : IRequestHandler<UpdateLicensePlateFromVehicleCommand, LicensePlate>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<LicensePlate> _validator;
        public UpdateLicensePlateFromVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<LicensePlate> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
        }
        public Task<LicensePlate> Handle(UpdateLicensePlateFromVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = _vehicleRepo.GetById(
                filter: x => x.Id.Equals(request._vehicleId),
                x => x.Include(s => s.LicensePlates));

            var results = _validator.Validate(request._licensePlate);
            var licenseplate = vehicle.LicensePlates.FirstOrDefault(s => s.Id == request._licensePlateId);

            if (results.IsValid == false)
            {
                request._errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                if(request._licensePlate.IsActive == true)
                {
                    foreach (var plate in vehicle.LicensePlates)
                    {
                        plate.IsActive = false;
                    }
                }
                licenseplate.Plate = request._licensePlate.Plate;
                licenseplate.IsActive = request._licensePlate.IsActive;
                _vehicleRepo.UpdateEntity(vehicle);
                _vehicleRepo.Save();
                return Task.FromResult(_mapper.Map<LicensePlate>(licenseplate));
            }
            return Task.FromResult(_mapper.Map<LicensePlate>(null));
        }
    }
}
