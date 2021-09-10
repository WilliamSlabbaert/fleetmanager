using System;
using DataLayer.entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class FleetManagerContext : DbContext
    {
        public DbSet<ChaffeurEntity> Chaffeurs { get; set; }
        public DbSet<FuelCardChaffeurEntity> FuelCardChaffeurs { get; set; }
        public DbSet<FuelCardEntity> FuelCards { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<MaintenanceEntity> Maintenances { get; set; }
        public DbSet<RepairmentEntity> Repairments { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"ConnectionString");
        }
    }
}
