using AutoMapper;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
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
    public class GetLicensePlateFromVehicleHandler : IRequestHandler<GetLicensePlateFromVehicleQuery, LicensePlate>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<LicensePlate> _validator;
        public GetLicensePlateFromVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<LicensePlate> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
        }
        public Task<LicensePlate> Handle(GetLicensePlateFromVehicleQuery request, CancellationToken cancellationToken)
        {
            var vehicle = _vehicleRepo.GetById(
                filter: s => s.Id == request.vehicleId,
                s => s.Include(x => x.LicensePlates));
            var licensePlate = vehicle.LicensePlates.FirstOrDefault(s => s.Id == request.licensePlateId);
            return Task.FromResult(_mapper.Map<LicensePlate>(licensePlate));
        }
    }
}
