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
        private DbSet<VehicleEntity> _table2 = null;

        public ChaffeurRepo(FleetManagerContext context)
        {
            _context = context;
            _table = context.Chaffeurs;
            _table2 = context.Vehicles;
        }
    }
}
