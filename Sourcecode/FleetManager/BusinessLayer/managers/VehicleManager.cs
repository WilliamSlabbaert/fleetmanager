using AutoMapper;
using BusinessLayer.managers.interfaces;
using BusinessLayer.models;
using DataLayer.entities;
using DataLayer.repositories;
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
            _vehicleRepo.AddEntity(_mapper.Map<VehicleEntity>(ch));
            _vehicleRepo.Save();
        }

        public Vehicle GetVehicleById(int id)
        {
            return _mapper.Map<Vehicle>(this._vehicleRepo.GetById(id));
        }

        public void UpdateVehicle(Vehicle ch)
        {
            _vehicleRepo.UpdateEntity(_mapper.Map<VehicleEntity>(ch));
        }
    }
}
