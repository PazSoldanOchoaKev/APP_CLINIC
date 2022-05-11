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

        public SingInViewModel(
            IAppNavigation navigation)
        {
            _navigation = navigation;
        }


        #region Properties

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
