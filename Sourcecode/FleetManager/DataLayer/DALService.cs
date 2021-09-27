using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class DALService
    {
        public static void AddDBContext(this IServiceCollection services, string connect)
        {
            services.AddDbContext<FleetManagerContext>(options => {
                options.UseSqlServer(connect);
            });
        }
    }
}
