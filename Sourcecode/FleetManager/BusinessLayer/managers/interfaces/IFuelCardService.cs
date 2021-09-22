using BusinessLayer.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.managers.interfaces
{
    public interface IFuelCardService
    {
        public FuelCard GetFuelCardById(int id);
        public void AddFuelCard(FuelCard fc);
        public void AddFuelCardToChaffeur(int fuelcardNr, int chaffeurNr);
        public void RemoveFuelCardFromChaffeur(int fuelcardNr, int chaffeurNr);
        public void UpdateFuelCard(FuelCard fc);
        public List<FuelCard> GetAllFuelCards();
    }
}
