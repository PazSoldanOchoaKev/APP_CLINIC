using System;
using AppointmentManager.Resources.Themes;
using AppointmentManager.ViewModels;
using AppointmentManager.ViewModels.Access;
using AppointmentManager.ViewModels.Pets;
using AppointmentManager.ViewModels.Register;
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
            services.AddViewModelToRoute<MainViewModel>("Main");
            services.AddViewModelToRoute<SignInViewModel>("SignIn");
            services.AddViewModelToRoute<NewAppoinmentViewModel>("New");
            //services.AddViewModelToRoute<AnimalInformationViewModel>("Registration");
            services.AddRoute<SignUpView, SignUpViewModel>();
            services.AddViewModelToRoute<PetsViewModel>("Pets");
            services.AddRoute<AnimalInformationView, AnimalInformationViewModel>();
            services
                .AddApiClient()
                .AcceptAnyServerCertificate()
                .ConfigureApiClient(options =>
                {
                    options.Url = "https://192.168.18.4:45455/api";
                });


            services.AddRoute<PersonalInformationView, PersonalInformationViewModel>();


        }
    }
}

