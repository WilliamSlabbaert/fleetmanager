using BusinessLayer.models;
using BusinessLayer.validators.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IFuelCardService
    {
        public List<GenericResponse> _errors { get; set; }
        public FuelCard GetFuelCardById(int id);
        public FuelCard AddFuelCard(FuelCard fc);
        public bool CheckExistingFuelCard(FuelCard fc);
        public FuelCard AddFuelType(int fuelcardId, FuelType type);
        public FuelCard DeleteFuelType(int id, int fuelid);
        public FuelCard AddService(ExtraService extraService, int fuelcardId);
        public FuelCard DeleteService(int id, int fuelcardId);
        public bool CheckValidationFuelCard(FuelCard fuelcard);
        public Chaffeur AddFuelCardToChaffeur(int fuelcardNr, int chaffeurNr);
        public Chaffeur UpdateChaffeurFuelCard(int fuelcardNr, int chaffeurNr, bool isactive);
        public void UpdateFuelCard(FuelCard fc);
        public List<FuelCard> GetAllFuelCards();
    }
}
