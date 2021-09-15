using AutoMapper;
using BusinessLayer.models;
using DataLayer.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MappingTool : Profile
    {
        public MappingTool()
        {
            CreateMap<ChaffeurEntity, Chaffeur>();
            CreateMap<VehicleEntity, Vehicle>();
            CreateMap<DrivingLicenseEntity, DrivingLicense>();
            CreateMap<AuthenticationTypeEntity, AuthenticationType>();
            CreateMap<ExtraServiceEntity, ExtraService>();
            CreateMap<FuelCardChaffeurEntity, FuelCardChaffeur>();
            CreateMap<FuelCardEntity, FuelCard>();
            CreateMap<FuelEntity, FuelType>();
            CreateMap<InvoiceEntity, Invoice>(); 
            CreateMap<LicensePlateEntity, LicensePlate>();
            CreateMap<MaintenanceEntity, Maintenance>();
            CreateMap<RepairmentEntity, Repairment>();
            CreateMap<RequestEntity, Request>();
            CreateMap<VehicleChaffeurEntity, VehicleChaffeur>();

            CreateMap<Chaffeur, ChaffeurEntity>();
            CreateMap<Vehicle, VehicleEntity>();
            CreateMap<DrivingLicense, DrivingLicenseEntity>();
            CreateMap<AuthenticationType, AuthenticationTypeEntity>();
            CreateMap<ExtraService, ExtraServiceEntity>();
            CreateMap<FuelCardChaffeur, FuelCardChaffeurEntity>();
            CreateMap<FuelCard, FuelCardEntity>();
            CreateMap<FuelType, FuelEntity>();
            CreateMap<Invoice, InvoiceEntity>();
            CreateMap<LicensePlate, LicensePlateEntity>();
            CreateMap<Maintenance, MaintenanceEntity>();
            CreateMap<Repairment, RepairmentEntity>();
            CreateMap<Request, RequestEntity>();
            CreateMap<VehicleChaffeur, VehicleChaffeurEntity>();
        }
    }
}
