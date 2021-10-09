using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using BusinessLayer.validators.response;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
        private readonly IValidator<Vehicle> _validator;
        public List<GenericResponse> _errors { get; set; }

        public VehicleService(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper, IValidator<Vehicle> validator)
        {
            _vehicleRepo = vehicleRepo;
            _mapper = mapper;
            _validator = validator;
            _errors = new List<GenericResponse>();
        }

        public void AddVehicle(Vehicle ch)
        {
            var results = _validator.Validate(ch);
            if(results.IsValid == false)
            {
                _errors = _mapper.Map<List<GenericResponse>>(results.Errors);
            }
            else
            {
                _vehicleRepo.AddEntity(_mapper.Map<VehicleEntity>(ch));
                _vehicleRepo.Save();
            }
        }

        public Vehicle GetVehicleById(int id)
        {
            return _mapper.Map<Vehicle>(_vehicleRepo.GetById(
                x => x.Id == id
                , x => x.Include(s => s.LicensePlates)
                 .Include(s => s.Requests)
                 .Include(s => s.LicensePlates)
                 .Include(s => s.ChaffeurVehicles)
                 .ThenInclude(s => s.Chaffeur)));
        }
        public List<Vehicle> GetAllVehicles()
        {
            var temp = new DataLayer.entities.paging.GenericParemeters();
            temp.PageSize = 3;
            temp.PageNumber = 2;
            return _mapper.Map<List<Vehicle>>(this._vehicleRepo.GetAllWithPaging(null,temp));
        }
        public void UpdateVehicle(Vehicle ch)
        {
            _vehicleRepo.UpdateEntity(_mapper.Map<VehicleEntity>(ch));
        }
    }
}
