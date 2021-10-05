using DataLayer.entities.generic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class ChaffeurEntityFuelCardEntity : IGeneralEntities
    {
        public ChaffeurEntityFuelCardEntity()
        {
        }

        public ChaffeurEntityFuelCardEntity(ChaffeurEntity chaffeur, FuelCardEntity fuelCard, bool isActive)
        {
            Chaffeur = chaffeur;
            FuelCard = fuelCard;
            IsActive = isActive;
        }


        public int ChaffeurId { get; set; }
        public ChaffeurEntity Chaffeur { get; set; }
        public int FuelCardId { get; set; }
        public FuelCardEntity FuelCard { get; set; }
        public bool IsActive { get; set; }
    }
}
