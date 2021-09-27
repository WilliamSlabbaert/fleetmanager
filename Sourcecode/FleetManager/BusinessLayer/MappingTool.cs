﻿using AutoMapper;
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
            CreateMap<ChaffeurEntity, Chaffeur>();
            CreateMap<VehicleEntity, Vehicle>();
            CreateMap<DrivingLicenseEntity, DrivingLicense>();
            CreateMap<AuthenticationTypeEntity, AuthenticationType>();
            CreateMap<ExtraServiceEntity, ExtraService>();
            CreateMap<ChaffeurEntityFuelCardEntity, FuelCardChaffeur>();
            CreateMap<FuelCardEntity, FuelCard>();
            CreateMap<FuelEntity, FuelType>();
            CreateMap<InvoiceEntity, Invoice>(); 
            CreateMap<LicensePlateEntity, LicensePlate>();
            CreateMap<MaintenanceEntity, Maintenance>();
            CreateMap<RepairmentEntity, Repairment>();
            CreateMap<RequestEntity, Request>();
            CreateMap<ChaffeurEntityVehicleEntity, VehicleChaffeur>();

            CreateMap<Chaffeur, ChaffeurEntity>();
            CreateMap<Vehicle, VehicleEntity>();
            CreateMap<DrivingLicense, DrivingLicenseEntity>();
            CreateMap<AuthenticationType, AuthenticationTypeEntity>();
            CreateMap<ExtraService, ExtraServiceEntity>();
            CreateMap<FuelCardChaffeur, ChaffeurEntityFuelCardEntity>();
            CreateMap<FuelCard, FuelCardEntity>();
            CreateMap<FuelType, FuelEntity>();
            CreateMap<Invoice, InvoiceEntity>();
            CreateMap<LicensePlate, LicensePlateEntity>();
            CreateMap<Maintenance, MaintenanceEntity>();
            CreateMap<Repairment, RepairmentEntity>();
            CreateMap<Request, RequestEntity>();
            CreateMap<VehicleChaffeur, ChaffeurEntityVehicleEntity>();

            CreateMap<ValidationFailure, GenericResponse>()
                .ForMember(s => s.Input, opt=> opt.MapFrom(src => src.AttemptedValue))
                .ForMember(s => s.Property, opt=> opt.MapFrom(src => src.PropertyName))
                .ForMember(s => s.Error, opt => opt.MapFrom(src => src.ErrorMessage));

        }
    }
}
