using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.repositories
{
    public class ChaffeurRepo 
    {
        FleetManagerContext context;

        public ChaffeurRepo(FleetManagerContext context)
        {
            this.context = context;
        }
        public ChaffeurEntity Get(int id)
        {
            return this.context.Chaffeurs.FirstOrDefault(s => s.Id == id);
        }
        public List<ChaffeurEntity> GetAll()
        {
            return this.context.Chaffeurs.ToList();
        }
        public void Add(ChaffeurEntity chaffeur)
        {
            if (chaffeur != null)
            {
                this.context.Chaffeurs.Add(chaffeur);
            }
        }
        public void Delete(int id)
        {
            this.context.Chaffeurs.Remove(this.Get(id));
        }
        public void Update(ChaffeurEntity chaffeur)
        {
            this.context.Entry(chaffeur).State = EntityState.Modified;

        }
    }
}
