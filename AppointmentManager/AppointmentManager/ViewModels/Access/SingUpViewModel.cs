using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Netcos.Mvvm;

namespace AppointmentManager.ViewModels.Access
{
    public class SingUpViewModel : ViewModelBase, INavigated
    {
        private string names;
        private ObservableCollection<string> typeDocuments;
        private string typeDocument;
        private string apellido;
        private string address;
        private string email;
        private string password;

        public SingUpViewModel()
        {
        }

        #region Properties

        public string Names { get => names; set => SetProperty(ref names, value); }
        public string Apellido { get => apellido; set => SetProperty(ref apellido, value); }
        public ObservableCollection<string> TypeDocuments { get => typeDocuments; set => SetProperty(ref typeDocuments, value); }
        public string TypeDocument { get => typeDocument; set => SetProperty(ref typeDocument, value); }
        //public 
        public string Address { get => address; set => SetProperty(ref address, value); }
        public string Email { get => email; set => SetProperty(ref email, value); }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public void OnNavigated()
        {
            var documents = new List<string> { "DNI", "CARNET DE EXTRANJERIA" };
            TypeDocuments = new ObservableCollection<string>(documents);
        }

        #endregion
    }
}
