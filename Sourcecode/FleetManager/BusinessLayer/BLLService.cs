using AutoMapper;
using BusinessLayer.managers;
using BusinessLayer.managers.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.validators;
using DataLayer;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class BLLService
    {
        public static void AddBLLService(this IServiceCollection services)
        {
            services.AddDBContext();
            AddBLLMapper(services);
            AddBLLManagers(services);
            AddBLLValidators(services);
            AddBLLMediator(services);
        }

        private static void AddBLLMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingTool());
            });
            services.AddSingleton(mappingConfig.CreateMapper());
        }

        private static void AddBLLManagers(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IChaffeurRepo, ChaffeurRepo>();
            services.AddScoped<IVehicleRepo, VehicleRepo>();
            services.AddScoped<IFuelCardRepo, FuelCardRepo>();

            services.AddScoped<IChaffeurService, ChaffeurService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IFuelCardService, FuelCardService>();
            services.AddScoped<IDrivingLicenseService, DrivingLicenseService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRepairmentService, RepairmentService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
        }
        private static void AddBLLValidators(IServiceCollection services)
        {
            services.AddScoped<IValidator<Chaffeur>, ChaffeurValidator>();
            services.AddScoped<IValidator<Vehicle>, VehicleValidator>();
            services.AddScoped<IValidator<Request>, RequestValidator>();
            services.AddScoped<IValidator<Maintenance>, MaintenanceValidator>();
            services.AddScoped<IValidator<Repairment>, RepairmentValidator>();
            services.AddScoped<IValidator<AuthenticationType>, AuthenticationValidator>();
            services.AddScoped<IValidator<DrivingLicense>, DrivingLicenseValidator>();
            services.AddScoped<IValidator<ExtraService>, ExtraServiceValidator>();
            services.AddScoped<IValidator<FuelCard>, FuelCardValidator>();
            services.AddScoped<IValidator<FuelType>, FuelTypeValidator>();
            services.AddScoped<IValidator<LicensePlate>, LicensePlateValidator>();
            services.AddScoped<IValidator<Invoice>, InvoiceValidator>();
            services.AddScoped<IValidator<VehicleChaffeur>, ChaffeurVehicleValidator>();
            services.AddScoped<IValidator<FuelCardChaffeur>, FuelCardChaffeurValidator>();
        }
        private static void AddBLLMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetVehiclesQuery).Assembly);
            services.AddMediatR(typeof(GetVehicleByIdQuery).Assembly);
            services.AddMediatR(typeof(AddVehicleCommand).Assembly);
        }
    }
}
