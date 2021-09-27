using AutoMapper;
using BusinessLayer.models;
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
            CreateMap<ChaffeurEntity, Chaffeur>().ReverseMap();
            CreateMap<VehicleEntity, Vehicle>().ReverseMap();
            CreateMap<DrivingLicenseEntity, DrivingLicense>().ReverseMap();
            CreateMap<AuthenticationTypeEntity, AuthenticationType>().ReverseMap();
            CreateMap<ExtraServiceEntity, ExtraService>().ReverseMap();
            CreateMap<ChaffeurEntityFuelCardEntity, FuelCardChaffeur>().ReverseMap();
            CreateMap<FuelCardEntity, FuelCard>().ReverseMap();
            CreateMap<FuelEntity, FuelType>().ReverseMap();
            CreateMap<InvoiceEntity, Invoice>().ReverseMap();
            CreateMap<LicensePlateEntity, LicensePlate>().ReverseMap();
            CreateMap<MaintenanceEntity, Maintenance>().ReverseMap();
            CreateMap<RepairmentEntity, Repairment>().ReverseMap();
            CreateMap<RequestEntity, Request>().ReverseMap();
            CreateMap<ChaffeurEntityVehicleEntity, VehicleChaffeur>().ReverseMap();

            CreateMap<ValidationFailure, GenericResponse>()
                .ForMember(s => s.Input, opt=> opt.MapFrom(src => src.AttemptedValue))
                .ForMember(s => s.Property, opt=> opt.MapFrom(src => src.PropertyName))
                .ForMember(s => s.Error, opt => opt.MapFrom(src => src.ErrorMessage));

        }
    }
}
