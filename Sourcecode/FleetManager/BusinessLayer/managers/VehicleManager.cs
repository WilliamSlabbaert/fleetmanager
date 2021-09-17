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
    public class VehicleManager : IVehicleManager
    {
        private readonly IGenericRepo<VehicleEntity> _vehicleRepo;
        private readonly IMapper _mapper;

        public VehicleManager(IGenericRepo<VehicleEntity> vehicleRepo, IMapper mapper)
        {
            _vehicleRepo = vehicleRepo;
            _mapper = mapper;
        }

        public void AddVehicle(Vehicle ch)
        {
            if (ch == null)
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
            return _mapper.Map<Vehicle>(GetAllVehicles()
                .Where(s => s.Id == id)
                .FirstOrDefault());
        }
        public List<Vehicle> GetAllVehicles()
        {
            String[] te = new string[] { "Chaffeurs" };

            return _mapper.Map<List<Vehicle>>(this._vehicleRepo.GetAll(te)
                .Include(s => s.Chaffeurs)
                .Include(s => s.LicensePlates)
                .Include(s => s.Requests)
                .Include(s => s.FuelTypes)
                );
        }
        public void UpdateVehicle(Vehicle ch)
        {
            _vehicleRepo.UpdateEntity(_mapper.Map<VehicleEntity>(ch));
        }
    }
}
