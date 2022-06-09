using System;
using System.Threading.Tasks;
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
        private readonly ILoadingFactory _loadingFactory;

        public SignInViewModel(
            IDisplay display,
            ILoadingFactory loadingFactory,
            IAppNavigation navigation,
            IApiClientFactory apiClientFactory)
        {
            _navigation = navigation;
            _display = display;
            _loadingFactory = loadingFactory;
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

            using (await _loadingFactory.ShowAsync("Inicio de sesión", "Espera un momento estamos validando tu usuario"))
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("account/SignIn")
                    .AddJsonBody(model)
                    .PostAsAsync<UserModel>();

                if (result)
                {
                    await _navigation
                        .GoToAsync("//Main")
                        .NotifyAsync(result.Value);
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
