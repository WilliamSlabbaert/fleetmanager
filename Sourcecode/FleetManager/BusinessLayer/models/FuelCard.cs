using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class FuelCard
    {
        public FuelCard()
        {
            AuthenticationTypes = new List<AuthenticationType>();
            FuelType = new List<FuelType>();
            Services = new List<ExtraService>();
            ChaffeurFuelCards = new List<FuelCardChaffeur>();
        }

        public FuelCard(string cardNumber, string pin, bool isActive)
        {
            CardNumber = cardNumber;
            Pin = pin;
            IsActive = isActive;
            AuthenticationTypes = new List<AuthenticationType>();
            FuelType = new List<FuelType>();
            Services = new List<ExtraService>();
            ChaffeurFuelCards = new List<FuelCardChaffeur>();
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public bool IsActive { get; set; }
        public List<AuthenticationType> AuthenticationTypes { get; set; }
        public List<FuelType> FuelType { get; set; }
        public List<ExtraService> Services { get; set; }
        public List<FuelCardChaffeur> ChaffeurFuelCards { get; set; }
    }
}
