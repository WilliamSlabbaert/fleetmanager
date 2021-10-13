using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models.general;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BusinessLayer.mediator.handlers.commands
{
    public class UpdateVehicleHandler : IRequestHandler<UpdateVehicleCommand, GenericResult<IGeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateVehicleHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IMediator mediator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._mediator = mediator;
        }
        public Task<GenericResult<IGeneralModels>> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            var result = _mediator.Send(new CheckExistingVehicleQuery(request.vehicle));
            var respond = new GenericResult<IGeneralModels>() { Message = "Vehicle with same chasis number already exists." };
            respond.SetStatusCode(Overall.ResponseType.BadRequest);
            if (result.Result == false)
            {
                return Task.FromResult(respond);
            }
            var vh = _vehicleRepo.GetById(s => s.Id == request.vehicleId, null);
            vh.Model = request.vehicle.Model;
            vh.Type = request.vehicle.Type;
            vh.Brand = request.vehicle.Brand;
            vh.Chassis = request.vehicle.Chassis;
            vh.FuelType = request.vehicle.FuelType;
            vh.BuildDate = request.vehicle.BuildDate;
            _vehicleRepo.UpdateEntity(vh);
            _vehicleRepo.Save();
            respond.SetStatusCode(Overall.ResponseType.OK);
            respond.Message = "Ok";
            respond.ReturnValue = vh;
            return Task.FromResult(respond);
        }
    }
}
