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
    public class AddLicensePlateToVehicleHandler : IRequestHandler<AddLicensePlateToVehicleCommand, LicensePlate>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<LicensePlate> _validator;
        public AddLicensePlateToVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<LicensePlate> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
        }

        Task<LicensePlate> IRequestHandler<AddLicensePlateToVehicleCommand, LicensePlate>.Handle(AddLicensePlateToVehicleCommand request, CancellationToken cancellationToken)
        {
            var temp = _vehicleRepo.GetById(
                filter: s => s.Id == request.vehicleId,
                s => s.Include(s => s.LicensePlates));

            var results = _validator.Validate(request.licensePlate);
            var temp2 = _mapper.Map<LicensePlateEntity>(request.licensePlate);
            if (results.IsValid == false)
            {
                request._errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                temp.LicensePlates.Add(temp2);
                _vehicleRepo.UpdateEntity(temp);
                _vehicleRepo.Save();
            }
            return Task.FromResult(_mapper.Map<LicensePlate>(temp2));
        }
    }
}
