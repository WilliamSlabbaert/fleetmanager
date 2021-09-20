using DataLayer.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public class FuelCardRepo : IFuelCardRepo
    {
        private FleetManagerContext _context = null;
        private DbSet<FuelCardEntity> _table = null;

        public FuelCardRepo(FleetManagerContext context)
        {
            _context = context;
            _table = context.FuelCards;
        }

        public void AddFuelCardToChaffeur(ChaffeurEntity ch, FuelCardEntity vh)
        {
           
        }
        public void RemoveFuelCardFromChaffeur(ChaffeurEntity ch, FuelCardEntity vh)
        {
        }
    }
}
