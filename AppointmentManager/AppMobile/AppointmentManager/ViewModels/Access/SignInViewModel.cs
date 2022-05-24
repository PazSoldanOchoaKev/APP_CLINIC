using System;
using System.Windows.Input;
using AppointmentManager.Models;
using AppointmentManager.Views.Access;
using Netcos.Extensions.Http;
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
        private readonly IApiClientFactory _apiClientFactory;

        public SignInViewModel(
            IDisplay display,
            IAppNavigation navigation,
            IApiClientFactory apiClientFactory)
        {
            _navigation = navigation;
            _display = display;
            _apiClientFactory = apiClientFactory;
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
            var model = new SignInModel
            {
                User = UserName,
                Password = Password
            };

            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("account/SignIn")
                    .AddJsonBody(model)
                    .PostAsync();

                if (result)
                {
                    await _navigation.GoToAsync("//Main");
                }
                else
                {
                    await _display.AlertAsync("Inicio de sesión", result.ErrorMessage);
                }

            }
        }
        #endregion
    }
}
