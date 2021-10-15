using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
using BusinessLayer.models.general;
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
    public class UpdateLicensePlateFromVehicleHandler : IRequestHandler<UpdateLicensePlateFromVehicleCommand, GenericResult<IGeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        public UpdateLicensePlateFromVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
        }
        public Task<GenericResult<IGeneralModels>> Handle(UpdateLicensePlateFromVehicleCommand request, CancellationToken cancellationToken)
        {
            var vehicle = _vehicleRepo.GetById(
                filter: x => x.Id.Equals(request._vehicleId),
                x => x.Include(s => s.LicensePlates));

            var dto = _mapper.Map<LicensePlate>(request._licensePlate);
            dto.Id = request._licensePlateId;

            var respond = new GenericResult<IGeneralModels>() { Message = "Licenseplate already exist's in vehicle list." };
            respond.SetStatusCode(Overall.ResponseType.BadRequest);

            var licenseplate = vehicle.LicensePlates.FirstOrDefault(s => s.Id == request._licensePlateId);

            var temp = _mapper.Map<Vehicle>(vehicle);

            if (temp.CheckLicensePlates(dto) == false)
            {
                return Task.FromResult(respond);
            }
            if (dto.IsActive == true)
            {
                foreach (var item in vehicle.LicensePlates)
                {
                    item.IsActive = false;
                }
            }

            licenseplate.Plate = dto.Plate;
            licenseplate.IsActive = dto.IsActive;
            _vehicleRepo.UpdateEntity(vehicle);
            _vehicleRepo.Save();
            respond.SetStatusCode(Overall.ResponseType.OK);
            respond.Message = "Ok";
            respond.ReturnValue = _mapper.Map<Vehicle>(vehicle);
            return Task.FromResult(respond);
        }
    }
}
