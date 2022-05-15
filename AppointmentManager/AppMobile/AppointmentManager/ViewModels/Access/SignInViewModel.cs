using System;
using System.Windows.Input;
using AppointmentManager.Views.Access;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IAppNavigation _navigation;
        private readonly IDisplay _display;
        private string userName;
        private string password;

        public SignInViewModel(
            IDisplay display,
            IAppNavigation navigation)
        {
            _navigation = navigation;
            _display = display;
        }


        #region Properties

        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        #endregion
        
        #region Commands
        public ICommand SignUp => new Command(NavigateToSignUp);
        public ICommand SignIn => new Command(NavigateToSignIn);

        #endregion

        #region Methods

        public async void NavigateToSignUp()
        {
            await _navigation.NavigateToAsync<SignUpView>();
        }
        public async void NavigateToSignIn()
        {
            //await _display.AlertAsync("Inicio de sesión", "Usuario incorrecto");
            //await _navigation.GoToAsync("//Main");

        }
        #endregion
    }
}
