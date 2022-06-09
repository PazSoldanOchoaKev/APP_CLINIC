using System;
using System.Linq;
using App.Infraestructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infraestructure
{
    public static class AppoinmentServiceCollectionExtensions
    {
        public static IServiceCollection AddAppoinmentIntraestructure(this IServiceCollection services)
        {
            services
                .AddDbContext<AppoinmentContext>("Appoinments")
                .UseSqlServer(options => options.);

            return services;
        }
    }
}
