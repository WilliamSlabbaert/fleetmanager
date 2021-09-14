﻿using System;
using DataLayer.entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class FleetManagerContext : DbContext
    {
        public DbSet<AuthenticationTypeEntity> AuthenticationTypes { get; set; }
        public DbSet<ChaffeurEntity> Chaffeurs { get; set; }
        public DbSet<DrivingLicenseEntity> DrivingLicenses { get; set; }
        public DbSet<ExtraServiceEntity> ExtraServices { get; set; }
        public DbSet<FuelCardEntity> FuelCards { get; set; }
        public DbSet<FuelEntity> Fuels { get; set; }
        public DbSet<InvoiceEntity> Invoices { get; set; }
        public DbSet<LicensePlateEntity> LicensePlates { get; set; }
        public DbSet<MaintenanceEntity> Maintenances { get; set; }
        public DbSet<RepairmentEntity> Repairments { get; set; }
        public DbSet<RequestEntity> Requests { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-2KEN9DG;Initial Catalog=FleetManagerTest;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChaffeurEntity>(entity =>
            {
                entity.HasMany(e => e.Requests)
                .WithOne(e => e.Chaffeur)
                .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<VehicleEntity>(entity =>
            {
                entity.HasMany(e => e.Requests)
                .WithOne(e => e.Vehicle)
                .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
