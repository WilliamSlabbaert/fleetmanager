using DataLayer.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public class VehicleRepo
    {
        FleetManagerContext context;

        public VehicleRepo(FleetManagerContext context)
        {
            this.context = context;
        }
        public VehicleEntity Get(int id)
        {
            return this.context.Vehicles.FirstOrDefault(s => s.Id == id);
        }
        public List<VehicleEntity> GetAll()
        {
            return this.context.Vehicles.ToList();
        }
        public void Add(VehicleEntity vehicle)
        {
            if (vehicle != null)
            {
                this.context.Vehicles.Add(vehicle);
            }
        }
        public void Delete(int id)
        {
            this.context.Vehicles.Remove(this.Get(id));
        }
        public void Update(VehicleEntity vehicle)
        {
            this.context.Entry(vehicle).State = EntityState.Modified;

        }
    }
}
