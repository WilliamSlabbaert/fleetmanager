using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class ChauffeurEntityVehicleEntity : IGeneralEntities
    {


        public int VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
        public int ChauffeurId { get; set; }
        public ChauffeurEntity Chauffeur { get; set; }
        public bool IsActive { get; set; }
        public ChauffeurEntityVehicleEntity(VehicleEntity vehicle, ChauffeurEntity chauffeur, bool isActive)
        {
            Vehicle = vehicle;
            Chauffeur = chauffeur;
            IsActive = isActive;
        }

        public ChauffeurEntityVehicleEntity()
        {
        }
    }
}
