using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Netcos.Mvvm;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    public class SignUpViewModel : ViewModelBase, INavigated
    {
        private string names;
        private ObservableCollection<string> typeDocuments;
        private string typeDocument;
        private string apellido;
        private string address;
        private string email;
        private string password;
        private string telefono;
        private string document;

        public SignUpViewModel()
        {
        }

        #region Properties

        public string Names { get => names; set => SetProperty(ref names, value); }
        public string Apellido { get => apellido; set => SetProperty(ref apellido, value); }
        public ObservableCollection<string> TypeDocuments { get => typeDocuments; set => SetProperty(ref typeDocuments, value); }
        public string TypeDocument { get => typeDocument; set => SetProperty(ref typeDocument, value); }
<<<<<<< HEAD:AppointmentManager/AppMobile/AppointmentManager/ViewModels/Access/SingUpViewModel.cs
=======
        public string Document { get => document; set => SetProperty(ref document, value); }
        public string Telefono { get => telefono; set => SetProperty(ref telefono, value); }
>>>>>>> 2223ca5055505193955efb68380304ab5b4962cb:AppointmentManager/AppointmentManager/ViewModels/Access/SignUpViewModel.cs
        public string Address { get => address; set => SetProperty(ref address, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }

<<<<<<< HEAD:AppointmentManager/AppMobile/AppointmentManager/ViewModels/Access/SingUpViewModel.cs
        #endregion

        #region Methods
=======
        public ICommand Confirmar => new Command(Confirm);
>>>>>>> 2223ca5055505193955efb68380304ab5b4962cb:AppointmentManager/AppointmentManager/ViewModels/Access/SignUpViewModel.cs

        public void OnNavigated()
        {
            TypeDocuments = new ObservableCollection<string>(new string[] { "DNI", "CARNET DE EXTRANJERIA" });
        }

        public void Confirm()
        {

        }

        #endregion
    }
}
