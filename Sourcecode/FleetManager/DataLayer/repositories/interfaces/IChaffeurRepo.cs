using DataLayer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public interface IChaffeurRepo
    {
        public void AddVehicleToChaffeur(ChaffeurEntity ch , VehicleEntity vh);
        public void RemoveVehicleToChaffeur(ChaffeurEntity ch, VehicleEntity vh);
    }
}
