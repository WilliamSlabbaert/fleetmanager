using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
using BusinessLayer.models.general;
using BusinessLayer.validators;
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
    public class AddLicensePlateToVehicleHandler : IRequestHandler<AddLicensePlateToVehicleCommand, GenericResult<GeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private IValidator<LicensePlate> _licenseplateValidator;
        public AddLicensePlateToVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper,
            LicensePlateValidator licensePlateValidator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._licenseplateValidator = licensePlateValidator;
        }

        public Task<GenericResult<GeneralModels>> Handle(AddLicensePlateToVehicleCommand request, CancellationToken cancellationToken)
        {
            var temp = _vehicleRepo.GetById(
                filter: s => s.Id == request.vehicleId,
                s => s.Include(s => s.LicensePlates));
            var licensePlate = _mapper.Map<LicensePlate>(request.licensePlate);
            var check = _licenseplateValidator.Validate(licensePlate);
            var result = GenericValidationCheck.CheckModel(check, "Licenseplate is invalid");
            if (check.IsValid)
            {
                var respond = new GenericResult<GeneralModels>() { Message = "Licenseplate already exist's in vehicle list." };
                respond.SetStatusCode(Overall.ResponseType.BadRequest);

                var temp2 = _mapper.Map<Vehicle>(temp);
                if (temp2.CheckLicensePlates(licensePlate))
                {
                    temp.LicensePlates.Add(_mapper.Map<LicensePlateEntity>(licensePlate));
                    _vehicleRepo.UpdateEntity(temp);
                    _vehicleRepo.Save();
                    respond.ReturnValue = temp;
                    respond.SetStatusCode(Overall.ResponseType.OK);
                    respond.Message = "Ok";
                    return Task.FromResult(respond);
                }

                return Task.FromResult(respond);
            }
            return Task.FromResult(result);
        }
    }
}
