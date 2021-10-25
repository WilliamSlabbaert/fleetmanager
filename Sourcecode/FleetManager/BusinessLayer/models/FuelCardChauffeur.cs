using BusinessLayer.models.general;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class FuelCardChaffeur : IGeneralModels
    {
        public int ChaffeurId { get; set; }
        public Chaffeur Chaffeur { get; set; }
        public int FuelCardId { get; set; }
        public FuelCard FuelCard { get; set; }
        public bool IsActive { get; set; }

        public FuelCardChaffeur(Chaffeur chaffeur, FuelCard fuelCard, bool isActive)
        {
            Chaffeur = chaffeur;
            FuelCard = fuelCard;
            IsActive = isActive;
        }
    }
}
