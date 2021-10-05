using AutoMapper;
using BusinessLayer.mediator.queries;
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
    public class GetVehiclesHandler : IRequestHandler<GetVehiclesQuery, GenericResult>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Vehicle> _validator;
        public GetVehiclesHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
        }


        Task<GenericResult> IRequestHandler<GetVehiclesQuery, GenericResult>.Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {

            var temp =  Task.FromResult(_mapper.Map<List<Vehicle>>(_vehicleRepo.GetAll(
                s => s.Include(s => s.ChaffeurVehicles)
                .ThenInclude(s => s.Chaffeur)
                .Include(s => s.LicensePlates)
                .Include(s => s.Requests)).ToList())).Result;
            var response = new GenericResult()
            {
                Message = "OK",
                ReturnValue = temp
            };
            response.SetStatusCode(Overall.ResponseType.OK);
            return Task.FromResult(response);
        }
    }
}
