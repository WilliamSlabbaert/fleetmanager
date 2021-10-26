using AutoMapper;
using BusinessLayer.mediator.queries;
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

namespace BusinessLayer.mediator.handlers.queries
{
    public class GetVehicleByIdFromChaffeurHandler : IRequestHandler<GetVehicleByIdFromChauffeurQuery, GenericResult<GeneralModels>>
    {

        private IMediator _mediator;
        public GetVehicleByIdFromChaffeurHandler(IMediator mediator)
        {
            this._mediator = mediator;
        }
        public Task<GenericResult<GeneralModels>> Handle(GetVehicleByIdFromChauffeurQuery request, CancellationToken cancellationToken)
        {
            var vehicle = GetVehicle(request._vehicleId).Result;
            var respond = new GenericResult<GeneralModels>() { Message = "Vehicle doesn't exist in chaffeurs list."};
            if (vehicle.StatusCode != 200)
            {
                return Task.FromResult(vehicle);
            }
            Vehicle temp = (Vehicle)vehicle.ReturnValue;
            var result = temp.ChauffeurVehicles.FirstOrDefault(s => s.Chauffeur.Id == request._chauffeurId);
            if(result == null)
            {
                respond.SetStatusCode(Overall.ResponseType.NotFound);
                return Task.FromResult(respond);
            }
            respond.SetStatusCode(Overall.ResponseType.OK);
            respond.Message = "Ok";
            respond.ReturnValue = result;
            return Task.FromResult(respond);
        }
        public async Task<GenericResult<GeneralModels>> GetVehicle(int id)
        {
            var vehicle = await _mediator.Send(new GetVehicleByIdQuery(id));
            return vehicle;
        }
    }
}
