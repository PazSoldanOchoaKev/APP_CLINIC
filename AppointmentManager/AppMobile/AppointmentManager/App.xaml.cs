using System;
using AppointmentManager.Resources.Themes;
using AppointmentManager.ViewModels;
using AppointmentManager.ViewModels.Access;
using AppointmentManager.Views.Access;
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
            services.AddApplicationLife<ApplicationLife>();
            services.AddDarkTheme<DarkTheme>();
            services.AddLightTeme<LightTheme>();

            services.AddInputLayout();
            services.AddViewModel<AppShellViewModel>();
            services.AddViewModelToRoute<SignInViewModel>("SignIn");
            services.AddRoute<SignUpView, SignUpViewModel>();
            
        }
    }
}

