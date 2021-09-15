using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.repositories
{
    public class ChaffeurRepo : IChaffeurRepo
    {
        private FleetManagerContext _context = null;
        private DbSet<ChaffeurEntity> _table = null;

        public ChaffeurRepo(FleetManagerContext context)
        {
            _context = context;
            _table = context.Chaffeurs;
                /*.Include(s => s.Vehicles)
                .Include(s => s.Requests)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.FuelCards)
                .ThenInclude(s => s.Chaffeurs)
                .ThenInclude(s => s.Vehicles);*/
        }

        public void AddVehicleToChaffeur(ChaffeurEntity ch, VehicleEntity vh)
        {
            ch.Vehicles.Add(vh);
            var tempvh = _context.Vehicles.FirstOrDefault(s=>s.Id == vh.Id);
            var tempch = _context.Chaffeurs.FirstOrDefault(s => s.Id == ch.Id);
            tempch.Vehicles.Add(tempvh);
            _table.Attach(tempch);
            _context.Entry(tempch).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void RemoveVehicleToChaffeur(ChaffeurEntity ch, VehicleEntity vh)
        {
            ch.Vehicles.Add(vh);
            var tempvh = _context.Vehicles;

            var tempch = _context.Chaffeurs.Include(s => s.Vehicles)
                .Include(s => s.Requests)
                .Include(s => s.DrivingLicenses)
                .Include(s => s.FuelCards)
                .ThenInclude(s => s.Chaffeurs)
                .ThenInclude(s => s.Vehicles)
                .FirstOrDefault();

            tempch.Vehicles.Remove(tempch.Vehicles.Single(s =>s.Id == vh.Id));
            _table.Attach(tempch);
            _context.Entry(tempch).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
