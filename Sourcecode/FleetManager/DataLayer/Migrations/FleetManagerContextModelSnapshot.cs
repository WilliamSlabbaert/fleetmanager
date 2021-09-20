﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(FleetManagerContext))]
    partial class FleetManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataLayer.entities.AuthenticationTypeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FuelCardId")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FuelCardId");

                    b.ToTable("AuthenticationTypes");
                });

            modelBuilder.Entity("DataLayer.entities.ChaffeurEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HouseNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalInsurenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chaffeurs");
                });

            modelBuilder.Entity("DataLayer.entities.ChaffeurEntityFuelCardEntity", b =>
                {
                    b.Property<int>("ChaffeurId")
                        .HasColumnType("int");

                    b.Property<int>("FuelCardId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("ChaffeurId", "FuelCardId");

                    b.HasIndex("FuelCardId");

                    b.ToTable("ChaffeurEntityFuelCardEntity");
                });

            modelBuilder.Entity("DataLayer.entities.ChaffeurEntityVehicleEntity", b =>
                {
                    b.Property<int>("ChaffeurId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("ChaffeurId", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ChaffeurEntityVehicleEntity");
                });

            modelBuilder.Entity("DataLayer.entities.DrivingLicenseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChaffeurId")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChaffeurId");

                    b.ToTable("DrivingLicenses");
                });

            modelBuilder.Entity("DataLayer.entities.ExtraServiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FuelCardId")
                        .HasColumnType("int");

                    b.Property<int>("Service")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FuelCardId");

                    b.ToTable("ExtraServices");
                });

            modelBuilder.Entity("DataLayer.entities.FuelCardEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("Pin")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("FuelCards");
                });

            modelBuilder.Entity("DataLayer.entities.FuelEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Fuel")
                        .HasColumnType("int");

                    b.Property<int>("FuelCardId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FuelCardId");

                    b.ToTable("Fuels");
                });

            modelBuilder.Entity("DataLayer.entities.InvoiceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("InvoiceImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaintenanceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaintenanceId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("DataLayer.entities.LicensePlateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Plate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("LicensePlates");
                });

            modelBuilder.Entity("DataLayer.entities.MaintenanceEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Garage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Maintenances");
                });

            modelBuilder.Entity("DataLayer.entities.RepairmentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RequestId");

                    b.ToTable("Repairments");
                });

            modelBuilder.Entity("DataLayer.entities.RequestEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChaffeurId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChaffeurId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("DataLayer.entities.VehicleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Chassis")
                        .HasColumnType("int");

                    b.Property<int>("FuelType")
                        .HasColumnType("int");

                    b.Property<double>("Kilometers")
                        .HasColumnType("float");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("DataLayer.entities.AuthenticationTypeEntity", b =>
                {
                    b.HasOne("DataLayer.entities.FuelCardEntity", "FuelCard")
                        .WithMany("AuthenticationTypes")
                        .HasForeignKey("FuelCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelCard");
                });

            modelBuilder.Entity("DataLayer.entities.ChaffeurEntityFuelCardEntity", b =>
                {
                    b.HasOne("DataLayer.entities.ChaffeurEntity", "Chaffeur")
                        .WithMany("ChaffeurFuelCards")
                        .HasForeignKey("ChaffeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.entities.FuelCardEntity", "FuelCard")
                        .WithMany("ChaffeurFuelCards")
                        .HasForeignKey("FuelCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chaffeur");

                    b.Navigation("FuelCard");
                });

            modelBuilder.Entity("DataLayer.entities.ChaffeurEntityVehicleEntity", b =>
                {
                    b.HasOne("DataLayer.entities.ChaffeurEntity", "Chaffeur")
                        .WithMany("ChaffeurVehicles")
                        .HasForeignKey("ChaffeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.entities.VehicleEntity", "Vehicle")
                        .WithMany("ChaffeurVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chaffeur");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DataLayer.entities.DrivingLicenseEntity", b =>
                {
                    b.HasOne("DataLayer.entities.ChaffeurEntity", "Chaffeur")
                        .WithMany("DrivingLicenses")
                        .HasForeignKey("ChaffeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chaffeur");
                });

            modelBuilder.Entity("DataLayer.entities.ExtraServiceEntity", b =>
                {
                    b.HasOne("DataLayer.entities.FuelCardEntity", "FuelCard")
                        .WithMany("Services")
                        .HasForeignKey("FuelCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelCard");
                });

            modelBuilder.Entity("DataLayer.entities.FuelEntity", b =>
                {
                    b.HasOne("DataLayer.entities.FuelCardEntity", "FuelCard")
                        .WithMany("FuelType")
                        .HasForeignKey("FuelCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FuelCard");
                });

            modelBuilder.Entity("DataLayer.entities.InvoiceEntity", b =>
                {
                    b.HasOne("DataLayer.entities.MaintenanceEntity", "Maintenance")
                        .WithMany("Invoices")
                        .HasForeignKey("MaintenanceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maintenance");
                });

            modelBuilder.Entity("DataLayer.entities.LicensePlateEntity", b =>
                {
                    b.HasOne("DataLayer.entities.VehicleEntity", "Vehicle")
                        .WithMany("LicensePlates")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DataLayer.entities.MaintenanceEntity", b =>
                {
                    b.HasOne("DataLayer.entities.RequestEntity", "Request")
                        .WithMany("Maintenance")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("DataLayer.entities.RepairmentEntity", b =>
                {
                    b.HasOne("DataLayer.entities.RequestEntity", "Request")
                        .WithMany("Repairment")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Request");
                });

            modelBuilder.Entity("DataLayer.entities.RequestEntity", b =>
                {
                    b.HasOne("DataLayer.entities.ChaffeurEntity", "Chaffeur")
                        .WithMany("Requests")
                        .HasForeignKey("ChaffeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.entities.VehicleEntity", "Vehicle")
                        .WithMany("Requests")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chaffeur");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DataLayer.entities.ChaffeurEntity", b =>
                {
                    b.Navigation("ChaffeurFuelCards");

                    b.Navigation("ChaffeurVehicles");

                    b.Navigation("DrivingLicenses");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("DataLayer.entities.FuelCardEntity", b =>
                {
                    b.Navigation("AuthenticationTypes");

                    b.Navigation("ChaffeurFuelCards");

                    b.Navigation("FuelType");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("DataLayer.entities.MaintenanceEntity", b =>
                {
                    b.Navigation("Invoices");
                });

            modelBuilder.Entity("DataLayer.entities.RequestEntity", b =>
                {
                    b.Navigation("Maintenance");

                    b.Navigation("Repairment");
                });

            modelBuilder.Entity("DataLayer.entities.VehicleEntity", b =>
                {
                    b.Navigation("ChaffeurVehicles");

                    b.Navigation("LicensePlates");

                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
