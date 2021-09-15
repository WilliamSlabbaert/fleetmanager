using AutoMapper;
using BusinessLayer.managers;
using BusinessLayer.managers.interfaces;
using DataLayer;
using DataLayer.entities;
using DataLayer.repositories;
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
            AddDBContext(services);
            AddBLLMapper(services);
            AddBLLManagers(services);
        }

        public static void AddBLLMapper(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingTool());
            });
            services.AddSingleton(mappingConfig.CreateMapper());
        }

        public static void AddBLLManagers(IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
            services.AddScoped<IChaffeurRepo, ChaffeurRepo>();
            services.AddScoped<IChaffeurManager, ChaffeurManager>();
            services.AddScoped<IVehicleManager, VehicleManager>();
        }

        public static void AddDBContext(IServiceCollection services)
        {
            services.AddDbContext<FleetManagerContext>(options => {
                options.UseSqlServer(@"Data Source=DESKTOP-2KEN9DG;Initial Catalog=FleetManagerTest;Integrated Security=True");
            });
        }
    }
}
