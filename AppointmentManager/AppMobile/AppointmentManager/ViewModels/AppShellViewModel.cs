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
        private bool isFlyoutOpen;

        #endregion


        #region Properties

        public ICommand AppoinmentCommand => new Command(NewAppoinment);

        public bool IsFlyoutOpen { get => isFlyoutOpen; set => SetProperty(ref isFlyoutOpen, value); }

        #endregion

        #region Methods

        public async void NewAppoinment()
        {
            IsFlyoutOpen = false;
            await _navigation.NavigateToAsync<NewAppoinmentView>();
        }

        #endregion
    }
}