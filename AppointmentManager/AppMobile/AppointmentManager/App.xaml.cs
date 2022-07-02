using System;
using AppointmentManager.Resources.Themes;
using AppointmentManager.ViewModels;
using AppointmentManager.ViewModels.Access;
using AppointmentManager.ViewModels.Pets;
using AppointmentManager.ViewModels.Register;
using AppointmentManager.Views.Access;
using AppointmentManager.Views.Pets;
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
            services.AddLocalizer();
            services.AddValidationForm();
            services.AddViewModel<AppShellViewModel>();
            services.AddViewModelToRoute<MainViewModel>("Main");
            services.AddViewModelToRoute<SignInViewModel>("SignIn");
            services.AddViewModelToRoute<NewAppoinmentViewModel>("New");
            services.AddRoute<SignUpView, SignUpViewModel>();
            services.AddViewModelToRoute<PetsViewModel>("Pets");
            services.AddViewModelToRoute<HistoryAppointmentViewModel>("HistoryAppointment");
            services.AddRoute<MainViewModel>();
            services.AddRoute<AnimalInformationView, AnimalInformationViewModel>();
            services.AddRoute<PersonalInformationView, PersonalInformationViewModel>();
            services.AddRoute<ModifyAnimalInformationView, ModifyAnimalInformationViewModel>();
            services
                .AddApiClient()
                .AcceptAnyServerCertificate()
                .ConfigureApiClient(options =>
                {
                    //options.Url = "http://192.168.18.17:45455/api";
                    options.Url = "http://192.168.18.4:45455/api";
                });
        }
    }
}

