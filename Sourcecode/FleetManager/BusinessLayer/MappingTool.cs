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
            CreateMap<ChauffeurEntity, Chaffeur>().ReverseMap();
            CreateMap<VehicleEntity, Vehicle>().ReverseMap();
            CreateMap<DrivingLicenseEntity, DrivingLicense>().ReverseMap();
            CreateMap<AuthenticationTypeEntity, AuthenticationType>().ReverseMap();
            CreateMap<ExtraServiceEntity, ExtraService>().ReverseMap();
            CreateMap<ChauffeurEntityFuelCardEntity, FuelCardChaffeur>().ReverseMap();
            CreateMap<FuelCardEntity, FuelCard>().ReverseMap();
            CreateMap<FuelEntity, FuelType>().ReverseMap();
            CreateMap<InvoiceEntity, Invoice>().ReverseMap();
            CreateMap<LicensePlateEntity, LicensePlate>().ReverseMap();
            CreateMap<MaintenanceEntity, Maintenance>().ReverseMap();
            CreateMap<RepairmentEntity, Repairment>().ReverseMap();
            CreateMap<RequestEntity, Request>().ReverseMap();
            CreateMap<ChauffeurEntityVehicleEntity, VehicleChaffeur>().ReverseMap();
            CreateMap<KilometerHistoryEntity, KilometerHistory>().ReverseMap();

            CreateMap<ValidationFailure, GenericResponse>()
                .ForMember(s => s.Input, opt=> opt.MapFrom(src => src.AttemptedValue))
                .ForMember(s => s.Property, opt=> opt.MapFrom(src => src.PropertyName))
                .ForMember(s => s.Error, opt => opt.MapFrom(src => src.ErrorMessage));

            CreateMap<ChaffeurDTO, Chaffeur>();
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
