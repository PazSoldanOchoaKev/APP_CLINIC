using System;
using AppointmentManager.ViewModels;
using AppointmentManager.ViewModels.Access;
using Microsoft.Extensions.DependencyInjection;
using Netcos.Xamarin.Forms;

namespace AppointmentManager
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override void RegisterServices(IServiceCollection services)
        {
            services.AddViewModel<AppShellViewModel>();
            services.AddViewModelToRoute<SingInViewModel>("SingIn");
            services.AddApplicationLife<ApplicationLife>();
        }
    }
}

