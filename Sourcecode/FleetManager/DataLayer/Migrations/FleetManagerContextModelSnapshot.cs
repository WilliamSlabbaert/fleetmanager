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

            modelBuilder.Entity("DataLayer.entities.ChauffeurEntity", b =>
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

                    b.ToTable("Chauffeurs");
                });

            modelBuilder.Entity("DataLayer.entities.ChauffeurEntityFuelCardEntity", b =>
                {
                    b.Property<int>("ChauffeurId")
                        .HasColumnType("int");

                    b.Property<int>("FuelCardId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("ChauffeurId", "FuelCardId");

                    b.HasIndex("FuelCardId");

                    b.ToTable("ChauffeurEntityFuelCardEntity");
                });

            modelBuilder.Entity("DataLayer.entities.ChauffeurEntityVehicleEntity", b =>
                {
                    b.Property<int>("ChauffeurId")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.HasKey("ChauffeurId", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ChauffeurEntityVehicleEntity");
                });

            modelBuilder.Entity("DataLayer.entities.DrivingLicenseEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChauffeurId")
                        .HasColumnType("int");

                    b.Property<int>("type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChauffeurId");

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

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ValidityDate")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("DataLayer.entities.KilometerHistoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Kilometers")
                        .HasColumnType("float");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehicleId");

                    b.ToTable("Kilometers");
                });

            modelBuilder.Entity("DataLayer.entities.LicensePlateEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

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

                    b.Property<int>("ChauffeurId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChauffeurId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("DataLayer.entities.VehicleEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Brand")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BuildDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Chassis")
                        .HasColumnType("int");

                    b.Property<int>("FuelType")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("DataLayer.entities.ChauffeurEntityFuelCardEntity", b =>
                {
                    b.HasOne("DataLayer.entities.ChauffeurEntity", "Chauffeur")
                        .WithMany("ChauffeurFuelCards")
                        .HasForeignKey("ChauffeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.entities.FuelCardEntity", "FuelCard")
                        .WithMany("ChauffeurFuelCards")
                        .HasForeignKey("FuelCardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chauffeur");

                    b.Navigation("FuelCard");
                });

            modelBuilder.Entity("DataLayer.entities.ChauffeurEntityVehicleEntity", b =>
                {
                    b.HasOne("DataLayer.entities.ChauffeurEntity", "Chauffeur")
                        .WithMany("ChauffeurVehicles")
                        .HasForeignKey("ChauffeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.entities.VehicleEntity", "Vehicle")
                        .WithMany("ChauffeurVehicles")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chauffeur");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DataLayer.entities.DrivingLicenseEntity", b =>
                {
                    b.HasOne("DataLayer.entities.ChauffeurEntity", "Chauffeur")
                        .WithMany("DrivingLicenses")
                        .HasForeignKey("ChauffeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chauffeur");
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

            modelBuilder.Entity("DataLayer.entities.KilometerHistoryEntity", b =>
                {
                    b.HasOne("DataLayer.entities.VehicleEntity", "Vehicle")
                        .WithMany("Kilometers")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehicle");
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
                    b.HasOne("DataLayer.entities.ChauffeurEntity", "Chauffeur")
                        .WithMany("Requests")
                        .HasForeignKey("ChauffeurId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.entities.VehicleEntity", "Vehicle")
                        .WithMany("Requests")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chauffeur");

                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("DataLayer.entities.ChauffeurEntity", b =>
                {
                    b.Navigation("ChauffeurFuelCards");

                    b.Navigation("ChauffeurVehicles");

                    b.Navigation("DrivingLicenses");

                    b.Navigation("Requests");
                });

            modelBuilder.Entity("DataLayer.entities.FuelCardEntity", b =>
                {
                    b.Navigation("AuthenticationTypes");

                    b.Navigation("ChauffeurFuelCards");

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
                    b.Navigation("ChauffeurVehicles");

                    b.Navigation("Kilometers");

                    b.Navigation("LicensePlates");

                    b.Navigation("Requests");
                });
#pragma warning restore 612, 618
        }
    }
}
