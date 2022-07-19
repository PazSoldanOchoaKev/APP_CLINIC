using System;
using App.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace App.Application
{
    public static class ApoinmentServiceCollectionExtensions
    {
        public static IServiceCollection AddAppoinmentApplication(this IServiceCollection services)
        {
            services.AddScoped<IAccountManager, AccountManager>();
            services.AddScoped<IPetsManager, PetsManager>();
            services.AddScoped<IAppointmentManager, AppointmentManager>();
            //services.AddScoped<IPaymentManager, PaymentManager>();
            return services;
        }
    }
}

