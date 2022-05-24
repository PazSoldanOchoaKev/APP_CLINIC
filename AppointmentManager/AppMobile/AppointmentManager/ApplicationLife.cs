using System;
using AppointmentManager.ViewModels;
using AppointmentManager.Views;
using Netcos.Extensions.Http;
using Netcos.Net.Http;
using Netcos.Xamarin.Forms;

namespace AppointmentManager
{
    public class ApplicationLife : IApplicationLife
    {
        private readonly IAppNavigation _navigation;

        public ApplicationLife(
            IAppNavigation navigation)
        {
            _navigation = navigation;
        }

        public void OnResume()
        {

        }

        public void OnSleep()
        {

        }

        public void OnStart()
        {
            _navigation.SetMain<AppShellView, AppShellViewModel>();
            _navigation.GoToAsync("//SingIn");
        }
    }
}
