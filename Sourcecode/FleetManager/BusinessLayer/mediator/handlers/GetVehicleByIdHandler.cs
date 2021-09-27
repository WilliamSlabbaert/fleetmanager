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
    public class GetVehicleByIdHandler : IRequestHandler<GetVehicleByIdQuery, Vehicle>
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<Vehicle> _validator;
        public GetVehicleByIdHandler(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator)
        {
            this._vehicleRepo = vehicleRepo;
            this._mapper = mapper;
            this._validator = validator;
        }
        public Task<Vehicle> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_mapper.Map<Vehicle>(
                _vehicleRepo.GetById(
                    filter: s=> s.Id == request.Id,
                    s => s.Include(s=> s.Requests)
                    .Include(s=>s.ChaffeurVehicles)
                    .ThenInclude(s=>s.Chaffeur)
                    .Include(s=>s.LicensePlates))
                ));
        }
    }
}
