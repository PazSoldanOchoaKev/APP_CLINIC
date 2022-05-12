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
        private string userName;
        private string password;

        public SingInViewModel(
            IAppNavigation navigation)
        {
            _navigation = navigation;
        }


        #region Properties

        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public ICommand SingUp => new Command(NavigateToSingUp);
        public ICommand SingIn => new Command(NavigateToSingIn);

        #endregion

        #region Methods

        public void NavigateToSingUp()
                    {
            _navigation.NavigateToAsync<SingUpView>();
        }

        public void NavigateToSingIn()
        {
            _navigation.GoToAsync("//Main");
        }
        #endregion
    }
}
