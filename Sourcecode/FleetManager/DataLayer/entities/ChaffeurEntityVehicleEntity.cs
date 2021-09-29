using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class ChaffeurEntityVehicleEntity : IGeneralEntities
    {


        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
        public int ChaffeurId { get; set; }
        public ChaffeurEntity Chaffeur { get; set; }
        public bool IsActive { get; set; }
        public ChaffeurEntityVehicleEntity(VehicleEntity vehicle, ChaffeurEntity chaffeur, bool isActive)
        {
            Vehicle = vehicle;
            Chaffeur = chaffeur;
            IsActive = isActive;
        }

        public ChaffeurEntityVehicleEntity()
        {
        }
    }
}
