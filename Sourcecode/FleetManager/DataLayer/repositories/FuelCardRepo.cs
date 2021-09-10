using DataLayer.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.repositories
{
    public class FuelCardRepo
    {
        FleetManagerContext context;

        public FuelCardRepo(FleetManagerContext context)
        {
            this.context = context;
        }
        public FuelCardEntity Get(int id)
        {
            return this.context.FuelCards.FirstOrDefault(s => s.Id == id);
        }
        public List<FuelCardEntity> GetAll()
        {
            return this.context.FuelCards.ToList();
        }
        public void Add(FuelCardEntity fuelCard)
        {
            if (fuelCard != null)
            {
                this.context.FuelCards.Add(fuelCard);
            }
        }
        public void Delete(int id)
        {
            this.context.FuelCards.Remove(this.Get(id));
        }
        public void Update(FuelCardEntity fuelCard)
        {
            this.context.Entry(fuelCard).State = EntityState.Modified;

        }
    }
}
