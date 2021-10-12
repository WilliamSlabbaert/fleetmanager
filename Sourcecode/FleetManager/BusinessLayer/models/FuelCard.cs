using BusinessLayer.models.general;
using Overall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.models
{
    public class FuelCard : IGeneralModels
    {
        public FuelCard()
        {
            AuthenticationTypes = new List<AuthenticationType>();
            FuelType = new List<FuelType>();
            Services = new List<ExtraService>();
            ChaffeurFuelCards = new List<FuelCardChaffeur>();
        }

        public FuelCard(string cardNumber, string pin, bool isActive,DateTime validityDate)
        {
            CardNumber = cardNumber;
            Pin = pin;
            IsActive = isActive;
            ValidityDate = validityDate;
            AuthenticationTypes = new List<AuthenticationType>();
            FuelType = new List<FuelType>();
            Services = new List<ExtraService>();
            ChaffeurFuelCards = new List<FuelCardChaffeur>();
        }
        public bool CheckExistingFuelType(FuelType fuelType)
        {
            var result = FuelType.FirstOrDefault(s => s.Fuel == fuelType.Fuel);
            if(result == null)
            {
                return true;
            }
            return false;
        }
        public bool CheckExistingSerives(ExtraService service)
        {
            var result = Services.FirstOrDefault(s=> s.Service == service.Service);
            if (result == null)
            {
                return true;
            }
            return false;
        }
        public bool CheckExistingAuthentications(AuthenticationType type)
        {
            var result = AuthenticationTypes.FirstOrDefault(s => s.type == type.type);
            if (result == null)
            {
                return true;
            }
            return false;
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public DateTime ValidityDate { get; set; }
        public bool IsActive { get; set; }
        public List<AuthenticationType> AuthenticationTypes { get; set; }
        public List<FuelType> FuelType { get; set; }
        public List<ExtraService> Services { get; set; }
        public List<FuelCardChaffeur> ChaffeurFuelCards { get; set; }
    }
}
