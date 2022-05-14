using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppointmentManager.Views.Access;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    public class SignUpViewModel : ViewModelBase, INavigated
    {
        private readonly IAppNavigation _navigation;
        private readonly IDisplay _display;
        private string names;
        private ObservableCollection<string> typeDocuments;
        private string typeDocument;
        private string apellido;
        private string address;
        private string email;
        private string password;
        private string telefono;
        private string document;
        private string repeatPassword;

        public SignUpViewModel(
            IDisplay display,
            IAppNavigation navigation)
        {
            _navigation = navigation;
            _display = display;
        }

        #region Properties
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string RepeatPassword { get => repeatPassword; set => SetProperty(ref repeatPassword, value); }
        #endregion

        #region Commands

        public ICommand Confirmar => new Command(Confirm);

        #endregion

        #region Methods

        public void OnNavigated()
        {
            //TypeDocuments = new ObservableCollection<string>(new string[] { "DNI", "CARNET DE EXTRANJERIA" });
        }

        public async void Confirm()
        {
            await _navigation.NavigateToAsync<PersonalInformationView>();
        }

        #endregion
    }
}
