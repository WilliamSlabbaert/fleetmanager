using AutoMapper;
using BusinessLayer.mediator.commands;
using BusinessLayer.models;
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
    public class DeleteKilometerHistoryHandler : IRequestHandler<DeleteKilometerHistoryCommand,GenericResult<GeneralModels>>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteKilometerHistoryHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IMediator mediator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._mediator = mediator;
        }

        public Task<GenericResult<GeneralModels>> Handle(DeleteKilometerHistoryCommand request, CancellationToken cancellationToken)
        {
            var vh = _vehicleRepo.GetById(s=> s.Id == request._vehicleId,s=> s.Include( s=> s.Kilometers));
            var respond = new GenericResult<GeneralModels>() { Message = "Kilometerhistory doesn't exist in vehicle list." };
            respond.SetStatusCode(Overall.ResponseType.NotFound);
            if (CheckKilometer(vh,request._kilometerId))
            {
                return Task.FromResult(respond);
            }
            var temp = vh.Kilometers.FirstOrDefault(s => s.Id == request._kilometerId);
            vh.Kilometers.Remove(temp);
            _vehicleRepo.UpdateEntity(vh);
            _vehicleRepo.Save();

            respond.ReturnValue = _mapper.Map<Vehicle>(vh);
            respond.SetStatusCode(Overall.ResponseType.OK);
            respond.Message = "Ok";
            return Task.FromResult(respond);
        }
        public bool CheckKilometer(VehicleEntity vehicle , int kilometerId)
        {
            var check = vehicle.Kilometers.FirstOrDefault(s => s.Id == kilometerId);
            return check == null ? true : false;
        }
        
    }
}
