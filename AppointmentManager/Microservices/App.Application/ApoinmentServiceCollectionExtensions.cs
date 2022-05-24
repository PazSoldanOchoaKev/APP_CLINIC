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
            return services;
        }
    }
}

