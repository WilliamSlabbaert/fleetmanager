using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class VehicleChaffeur
    {

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
        public int ChaffeurId { get; set; }
        public Chaffeur Chaffeur { get; set; }
        public bool IsActive { get; set; }

        public VehicleChaffeur(Vehicle vehicle, Chaffeur chaffeur, bool isActive)
        {
            Vehicle = vehicle;
            Chaffeur = chaffeur;
            IsActive = isActive;
        }
    }
}
