using AutoMapper;
using BusinessLayer.managers;
using BusinessLayer.managers.interfaces;
using BusinessLayer.validators;
using DataLayer;
using DataLayer.entities;
using DataLayer.repositories;
using FluentValidation;
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
            DALService.AddDBContext(services);
            AddBLLMapper(services);
            AddBLLManagers(services);
            AddBLLValidators(services);
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

            services.AddScoped<IChaffeurManager, ChaffeurManager>();
            services.AddScoped<IVehicleManager, VehicleManager>();
            services.AddScoped<IFuelCardManager, FuelCardManager>();
            services.AddScoped<IDrivingLicenseManager, DrivingLicenseManager>();
            services.AddScoped<IRequestManager, RequestManager>();
            services.AddScoped<IRepairmentManager, RepairmentManager>();
        }
        private static void AddBLLValidators(IServiceCollection services)
        {
            //services.AddScoped<IValidator<T>, UserValidator>()
            services.AddScoped<IValidator<Chaffeur>, ChaffeurValidator>();
        }
    }
}
