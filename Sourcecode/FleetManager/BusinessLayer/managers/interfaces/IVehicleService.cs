using AutoMapper;
using BusinessLayer.models;
using BusinessLayer.validators.response;
using Overall.paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IVehicleService
    {
        public Vehicle GetVehicleById(int id);
        public object GetHeaders(GenericParameter parameters);
        public void AddVehicle(Vehicle ch);
        public void UpdateVehicle(Vehicle ch);
        public List<Vehicle> GetAllVehicles();
    }
}
