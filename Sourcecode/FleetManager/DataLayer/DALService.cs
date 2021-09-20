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
        public static void AddDBContext(IServiceCollection services)
        {
            services.AddDbContext<FleetManagerContext>(options => {
                options.UseSqlServer(@"Data Source=DESKTOP-2KEN9DG;Initial Catalog=FleetManagerTest;Integrated Security=True");
            });
        }
    }
}
