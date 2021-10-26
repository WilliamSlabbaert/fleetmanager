using AutoMapper;
using BusinessLayer.services;
using BusinessLayer.services.interfaces;
using BusinessLayer.mediator.commands;
using BusinessLayer.mediator.queries;
using BusinessLayer.models;
using BusinessLayer.validators;
using BusinessLayer.validators.mediator;
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
        public static void AddBLLService(this IServiceCollection services,string connect)
        {
            services.AddDBContext(connect: connect);
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

            services.AddScoped<IChauffeurService, ChauffeurService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IFuelCardService, FuelCardService>();
            services.AddScoped<IDrivingLicenseService, DrivingLicenseService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRepairmentService, RepairmentService>();
            services.AddScoped<IMaintenanceService, MaintenanceService>();
        }
        private static void AddBLLValidators(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<FuelCardChauffeurValidator>(ServiceLifetime.Scoped);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
        private static void AddBLLMediator(IServiceCollection services)
        {
            services.AddMediatR(typeof(GetVehiclesQuery).Assembly);
        }
    }
}
