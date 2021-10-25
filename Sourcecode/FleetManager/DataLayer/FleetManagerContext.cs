using System;
using DataLayer.entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class FleetManagerContext : DbContext
    {
        public DbSet<AuthenticationTypeEntity> AuthenticationTypes { get; set; }
        public DbSet<ChauffeurEntity> Chauffeurs { get; set; }
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
        public DbSet<KilometerHistoryEntity> Kilometers { get; set; }
        public FleetManagerContext(DbContextOptions<FleetManagerContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChauffeurEntityVehicleEntity>(entity =>
            {
                entity.HasKey(bc => new { bc.ChauffeurId, bc.VehicleId });
            });
            modelBuilder.Entity<ChauffeurEntityFuelCardEntity>(entity =>
            {
                entity.HasKey(bc => new { bc.ChauffeurId, bc.FuelCardId });
            });
        }
    }
}
