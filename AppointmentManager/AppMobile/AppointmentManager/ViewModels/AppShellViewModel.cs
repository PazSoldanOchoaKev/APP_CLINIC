using System;
using System.Windows.Input;
using AppointmentManager.Models;
using AppointmentManager.Views.Register;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels
{
    public class AppShellViewModel : ViewModelBase
    {
        private readonly IAppNavigation _navigation;

        public AppShellViewModel(
            IAppNavigation navigation)
        {
            _navigation = navigation;
        }
        #region Fields

        private ContentApp app;

        #endregion


        #region Properties

        public ICommand Naigation => new Command(Navegar);

        #endregion

        #region Methods

        public async void Navegar()
        {
            await _navigation.NavigateToAsync<NewAppoinmentView>();
        }

        #endregion
    }
}