using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class ChaffeurEntityFuelCardEntity
    {
        public ChaffeurEntityFuelCardEntity()
        {
        }

        public ChaffeurEntityFuelCardEntity(ChaffeurEntity chaffeur, FuelCardEntity fuelCard, bool isActive)
        {
            Chaffeur = chaffeur;
            ChaffeurId = chaffeur.Id;
            FuelCard = fuelCard;
            FuelCardId = fuelCard.Id;
            IsActive = isActive;
        }


        public int ChaffeurId { get; set; }
        public ChaffeurEntity Chaffeur { get; set; }
        public int FuelCardId { get; set; }
        public FuelCardEntity FuelCard { get; set; }
        public bool IsActive { get; set; }
    }
}
