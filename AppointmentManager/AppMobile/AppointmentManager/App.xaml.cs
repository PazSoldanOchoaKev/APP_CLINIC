using System;
using AppointmentManager.Resources.Themes;
using AppointmentManager.ViewModels;
using AppointmentManager.ViewModels.Access;
using AppointmentManager.ViewModels.Pets;
using AppointmentManager.ViewModels.Register;
using AppointmentManager.Views.Access;
using AppointmentManager.Views.Register;
using AppointmentManager.Views.Pets;
using Microsoft.Extensions.DependencyInjection;
using Netcos.Xamarin.Forms;
using AppointmentManager.ViewModels.Payment;
using AppointmentManager.ViewModels.User;

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
            services.AddRoute<SignUpView, SignUpViewModel>();
            services.AddViewModelToRoute<PetsViewModel>("Pets");
            services.AddViewModelToRoute<HistoryAppointmentViewModel>("HistoryAppointment");
            services.AddRoute<MainViewModel>();
            services.AddRoute<AnimalInformationView, AnimalInformationViewModel>();
            services.AddRoute<PersonalInformationView, PersonalInformationViewModel>();
            services.AddRoute<NewAppoinmentView, NewAppoinmentViewModel>();
            services.AddViewModelToRoute<PaymentFormViewModel>("Pay");
            services.AddViewModelToRoute<ProfileViewModel>("Profile");
            services
                .AddApiClient()
                .AcceptAnyServerCertificate()
                .ConfigureApiClient(options =>
                {
                   options.Url = "http://192.168.18.17:45455/api";
                    //options.Url = "http://192.168.18.4:45455/api";
                });
        }
    }
}

