using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers
{
    public class VehicleService : IVehicleService
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;
        private IMediator _mediator;

        public VehicleService(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator, IMediator mediator)
        {
            _vehicleRepo = vehicleRepo;
            _mapper = mapper;
            _mediator = mediator;
        }
        public Vehicle GetVehicleById(int id)
        {
            return _mapper.Map<Vehicle>(_vehicleRepo.GetById(
                x => x.Id == id
                , x => x.Include(s => s.LicensePlates)
                 .Include(s => s.Requests)
                 .Include(s => s.LicensePlates)
                 .Include(s => s.ChauffeurVehicles)
                 .ThenInclude(s => s.Chauffeur)));
        }
        public List<Vehicle> GetAllVehicles()
        {
            var temp = new GenericParameter();
            temp.PageSize = 3;
            temp.PageNumber = 2;
            return _mapper.Map<List<Vehicle>>(this._vehicleRepo.GetAllWithPaging(null,temp));
        }
        public void UpdateVehicle(Vehicle ch)
        {
            _vehicleRepo.UpdateEntity(_mapper.Map<VehicleEntity>(ch));
        }
        public object GetHeaders(GenericParameter parameters)
        {
            var temp = _vehicleRepo.GetAll(null);
            var temp2 = _mediator.Send(new GetHeadersQuery(parameters, temp)).Result;
            var metadata = new
            {
                temp2.TotalCount,
                temp2.PageSize,
                temp2.CurrentPage,
                temp2.HasNext,
                temp2.HasPrevious
            };
            return metadata;
        }
    }
}
