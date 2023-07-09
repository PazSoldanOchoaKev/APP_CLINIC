using System;
using System.Linq;
using App.Domain.Entities;
using App.Infraestructure.Data;
using App.Infraestructure.Data.Seed;
using Microsoft.Extensions.DependencyInjection;

namespace App.Infraestructure
{
    public static class AppoinmentServiceCollectionExtensions
    {
        public static IServiceCollection AddAppoinmentIntraestructure(this IServiceCollection services)
        {
            services
                .AddDbContext<AppoinmentContext>("Appoinments")
                .UseSqlServer()
                .AddEntitySeed<ProcedureTypes, ProcedureTypeSeed>()
                .AddEntitySeed<Doctors, DoctorsSeed>();

            return services;
        }
    }
}
