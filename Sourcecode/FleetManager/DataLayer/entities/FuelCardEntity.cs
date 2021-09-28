using DataLayer.entities.generic;
using Overall;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.entities
{
    public class FuelCardEntity : IGeneralEntities
    {
        [Key]
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public bool IsActive { get; set; }
        public List<FuelEntity> FuelType { get; set; }
        public List<ExtraServiceEntity> Services { get; set; }
        public List<ChaffeurEntityFuelCardEntity> ChaffeurFuelCards { get; set; }
        public List<AuthenticationTypeEntity> AuthenticationTypes { get; set; }

        public FuelCardEntity()
        {
            AuthenticationTypes = new List<AuthenticationTypeEntity>();
            FuelType = new List<FuelEntity>();
            Services = new List<ExtraServiceEntity>();
            ChaffeurFuelCards = new List<ChaffeurEntityFuelCardEntity>();
        }

        public FuelCardEntity(string cardNumber, string pin, bool isActive)
        {
            CardNumber = cardNumber;
            Pin = pin;
            IsActive = isActive;
            AuthenticationTypes = new List<AuthenticationTypeEntity>();
            FuelType = new List<FuelEntity>();
            Services = new List<ExtraServiceEntity>();
            ChaffeurFuelCards = new List<ChaffeurEntityFuelCardEntity>();
        }
    }
}
