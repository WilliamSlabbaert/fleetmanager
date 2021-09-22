using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using DataLayer.entities;
using DataLayer.repositories;
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

        public VehicleService(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper)
        {
            _vehicleRepo = vehicleRepo;
            _mapper = mapper;
        }

        public void AddVehicle(Vehicle ch)
        {
            if (ch != null)
            {
                _vehicleRepo.AddEntity(_mapper.Map<VehicleEntity>(ch));
                _vehicleRepo.Save();
            }
            else
            {
                throw new Exception("Vehicle is null.");
            }
        }

        public Vehicle GetVehicleById(int id)
        {
            return _mapper.Map<Vehicle>(_vehicleRepo.GetById(
                x => x.Id == id
                ,x => x.Include(s => s.LicensePlates)
                .Include(s => s.Requests)
                .Include(s => s.LicensePlates)
                .Include(s => s.ChaffeurVehicles)
                .ThenInclude(s => s.Chaffeur))); 
        }
        public List<Vehicle> GetAllVehicles()
        {
            return _mapper.Map<List<Vehicle>>(this._vehicleRepo.GetAll(
                x => x.Include(s => s.LicensePlates)
                .Include(s => s.Requests)
                .Include(s => s.LicensePlates)
                .Include(s => s.ChaffeurVehicles)
                .ThenInclude(s => s.Chaffeur)));
        }
        public void UpdateVehicle(Vehicle ch)
        {
            _vehicleRepo.UpdateEntity(_mapper.Map<VehicleEntity>(ch));
        }
    }
}
