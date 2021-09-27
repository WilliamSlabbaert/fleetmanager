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
    public class GetVehiclesHandler : IRequestHandler<GetVehiclesQuery, List<Vehicle>>
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
        public Task<List<Vehicle>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mapper.Map<List<Vehicle>>(_vehicleRepo.GetAll(
                s=> s.Include(s=> s.ChaffeurVehicles)
                .Include(s=>s.LicensePlates)
                .Include(s=>s.Requests)).ToList()));
        }
    }
}
