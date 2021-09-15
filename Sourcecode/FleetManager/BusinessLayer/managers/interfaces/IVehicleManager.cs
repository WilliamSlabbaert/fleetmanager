using AutoMapper;
using BusinessLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IVehicleManager
    {
        public Vehicle GetVehicleById(int id);
        public void AddVehicle(Vehicle ch);
        public void UpdateVehicle(Vehicle ch);
    }
}
