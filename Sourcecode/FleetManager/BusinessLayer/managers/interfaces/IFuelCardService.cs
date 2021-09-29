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
        public void AddFuelCardToChaffeur(int fuelcardNr, int chaffeurNr);
        public void UpdateFuelCard(FuelCard fc);
        public List<FuelCard> GetAllFuelCards();
    }
}
