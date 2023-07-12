using System;
using System.Threading.Tasks;
using System.Windows.Input;
using AppointmentManager.Models;
using AppointmentManager.Views.Access;
using FluentValidation;
using Netcos.Extensions.Http;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    public class SignInViewModel : ViewModelBase, INavigated
    {
        private readonly IAppNavigation _navigation;
        private readonly IDisplay _display;
        private string userName;
        private string password;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly ILoadingFactory _loadingFactory;
        private readonly IValidationFactory _validationFactory;

        public SignInViewModel(
            IDisplay display,
            ILoadingFactory loadingFactory,
            IValidationFactory validationFactory,
            IAppNavigation navigation,
            IApiClientFactory apiClientFactory)
        {
            _navigation = navigation;
            _display = display;
            _loadingFactory = loadingFactory;
            _apiClientFactory = apiClientFactory;
            _validationFactory = validationFactory;
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

        private bool signInValidate()
        {
            return _validationFactory.Create<SignInViewModel>("SignInForm")
                .AddRule(p => p.UserName, _ => _
                    .NotEmpty()
                    .WithMessage("Ingrese su usuario"))
                .AddRule(p => p.Password, p => p
                    .NotEmpty()
                    .WithMessage("Ingresa tu contraseña"))
                .Validate();
        }

        public async void NavigateToSignIn()
        {
            if (signInValidate())
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
        }

        public void OnNavigated()
        {
#if DEBUG
            UserName = "spazsoldanochoa@gmail.com";
            Password ="271496";
            // UserName = "stalin_kevin@hotmail.com";
            //  Password = "p4SSword*123";
#endif
        }
        #endregion
    }
}
