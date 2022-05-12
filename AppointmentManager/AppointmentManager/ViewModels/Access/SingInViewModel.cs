using System;
using System.Windows.Input;
using AppointmentManager.Views.Access;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    public class SingInViewModel : ViewModelBase
    {
        private readonly IAppNavigation _navigation;
        private readonly IDisplay _display;
        private string userName;
        private string password;

        public SingInViewModel(
            IDisplay display,
            IAppNavigation navigation)
        {
            _navigation = navigation;
            _display = display;
        }


        #region Properties

        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public ICommand SingUp => new Command(NavigateToSingUp);
        public ICommand SingIn => new Command(NavigateToSingIn);

        #endregion

        #region Methods

        public async void NavigateToSingUp()
        {
            await _navigation.NavigateToAsync<SingUpView>();
        }

        public async void NavigateToSingIn()
        {
            //await _display.AlertAsync("Inicio de sesión", "Usuario incorrecto");
            await _navigation.GoToAsync("//Main");

        }
        #endregion
    }
}
