using AppointmentManager.Models;
using Netcos.Extensions.Http;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
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
        private ObservableCollection<DocumentTypeModel> typeDocuments;
        private DocumentTypeModel typeDocument;
        private string document;
        private string telefono;
        private string address;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IDisplay _display;
        public PersonalInformationViewModel(
            IApiClientFactory apiClientFactory,
            IDisplay display)
        {
            _apiClientFactory = apiClientFactory;
        }

        #region Properties
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Apellido { get => apellido; set => SetProperty(ref apellido, value); }
        public ObservableCollection<DocumentTypeModel> TypeDocuments { get => typeDocuments; set => SetProperty(ref typeDocuments, value); }
        public DocumentTypeModel TypeDocument { get => typeDocument; set => typeDocument = value; }
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
            TypeDocuments = new ObservableCollection<DocumentTypeModel>(new DocumentTypeModel[] {
                new DocumentTypeModel { Type = DocumentType.DNI, Name =  "DNI" },
                new DocumentTypeModel { Type = DocumentType.CE, Name = "CARNET DE EXTRANJERIA" }
            });
        }

        public async void Confirm()
        {
            Model.FirstName = Name;
            Model.LastName = Apellido;
            Model.TypeDocument = TypeDocument.Type;
            Model.Document = Document;
            Model.PhoneNumber = Telefono;
            Model.Address = Address;

            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("account")
                    .AddJsonBody(Model)
                    .PostAsync();
            }
        }

        public void OnNotify(SignUpModel model)
        {
            Model = model;
        }

        #endregion

    }
}
