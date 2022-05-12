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

        public SingInViewModel(
            IAppNavigation navigation)
        {
            _navigation = navigation;
        }


        #region Properties

        public string UserName { get => userName; set => SetProperty(ref userName, value); }
        public ICommand SingUp => new Command(NavigateToSingUp);

        #endregion

        #region Methods

        public void NavigateToSingUp()
        {
            _navigation.NavigateToAsync<SingUpView>();
        }

        #endregion
    }
}
