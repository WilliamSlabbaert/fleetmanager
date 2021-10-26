using AutoMapper;
using BusinessLayer.models;
using BusinessLayer.models.input;
using BusinessLayer.validators.response;
using DataLayer.entities;
using FluentValidation.Results;
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
            CreateMap<ChauffeurEntity, Chauffeur>().ReverseMap();
            CreateMap<VehicleEntity, Vehicle>().ReverseMap();
            CreateMap<DrivingLicenseEntity, DrivingLicense>().ReverseMap();
            CreateMap<AuthenticationTypeEntity, AuthenticationType>().ReverseMap();
            CreateMap<ExtraServiceEntity, ExtraService>().ReverseMap();
            CreateMap<ChauffeurEntityFuelCardEntity, FuelCardChauffeur>().ReverseMap();
            CreateMap<FuelCardEntity, FuelCard>().ReverseMap();
            CreateMap<FuelEntity, FuelType>().ReverseMap();
            CreateMap<InvoiceEntity, Invoice>().ReverseMap();
            CreateMap<LicensePlateEntity, LicensePlate>().ReverseMap();
            CreateMap<MaintenanceEntity, Maintenance>().ReverseMap();
            CreateMap<RepairmentEntity, Repairment>().ReverseMap();
            CreateMap<RequestEntity, Request>().ReverseMap();
            CreateMap<ChauffeurEntityVehicleEntity, VehicleChauffeur>().ReverseMap();
            CreateMap<KilometerHistoryEntity, KilometerHistory>().ReverseMap();

            CreateMap<ChauffeurDTO, Chauffeur>();
            CreateMap<DrivingLicenseDTO, DrivingLicense>();
            CreateMap<RequestDTO, Request>();
            CreateMap<MaintenanceDTO, Maintenance>();
            CreateMap<RepairmentDTO, Repairment>();
            CreateMap<FuelTypeDTO, FuelType>();
            CreateMap<FuelCardDTO, FuelCard>();
            CreateMap<ExtraServiceDTO, ExtraService>();
            CreateMap<AuthenticationTypeDTO, AuthenticationType>();
            CreateMap<InvoiceDTO, Invoice>();
            CreateMap<VehicleDTO, Vehicle>();
            CreateMap<LicensePlateDTO, LicensePlate>();
            CreateMap<KilometerHistoryDTO, KilometerHistory>();

        }
    }
}
