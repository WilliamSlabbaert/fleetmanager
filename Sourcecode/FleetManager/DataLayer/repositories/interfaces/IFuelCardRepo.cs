using DataLayer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public interface IFuelCardRepo
    {
        public void AddFuelCardToChaffeur(ChaffeurEntity ch, FuelCardEntity vh);
        public void RemoveFuelCardFromChaffeur(ChaffeurEntity ch, FuelCardEntity vh);

    }
}
