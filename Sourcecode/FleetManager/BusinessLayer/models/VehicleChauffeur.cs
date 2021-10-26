using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class VehicleChauffeur 
    {

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int ChauffeurId { get; set; }
        public Chauffeur Chauffeur { get; set; }
        public bool IsActive { get; set; }

        public VehicleChauffeur(Vehicle vehicle, Chauffeur chaffeur, bool isActive)
        {
            Vehicle = vehicle;
            Chauffeur = chaffeur;
            IsActive = isActive;
        }

        public VehicleChauffeur()
        {
        }
    }
}
