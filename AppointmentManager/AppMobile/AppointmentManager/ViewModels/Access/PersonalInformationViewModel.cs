using AppointmentManager.Models;
using Netcos.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    public class PersonalInformationViewModel : ViewModelBase, INavigated, INotify<SignUpModel>
    {
        private string name;
        private string apellido;
        private ObservableCollection<string> typeDocuments;
        private string typeDocument;
        private string document;
        private string telefono;
        private string address;

        public PersonalInformationViewModel()
        {
        }
        #region Properties
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Apellido { get => apellido; set => SetProperty(ref apellido, value); }
        public ObservableCollection<string> TypeDocuments { get => typeDocuments; set => SetProperty(ref typeDocuments, value); }
        public string TypeDocument { get => typeDocument; set => typeDocument = value; }
        public string Document { get => document; set => SetProperty(ref document, value); }
        public string Telefono { get => telefono; set => SetProperty(ref telefono, value); }
        public string Address { get => address; set => SetProperty(ref address, value); }

        private SignUpModel Model { get; set; }

        #endregion

        #region Commands
        public ICommand Confirmar => new Command(Confirm);

        #endregion

        #region Methodos
        public void OnNavigated()
        {
            TypeDocuments = new ObservableCollection<string>(new string[] { "DNI", "CARNET DE EXTRANJERIA" });
        }

        public void Confirm()
        {
            Model.Name = Name;
            Model.Apellido = Apellido;
            Model.TypeDocument = TypeDocument;
            Model.Document = Document;
            Model.Telefono = Telefono;
            Model.Address = Address;
        }

        public void OnNotify(SignUpModel model)
        {
            Model = model;
        }

        #endregion

    }
}
