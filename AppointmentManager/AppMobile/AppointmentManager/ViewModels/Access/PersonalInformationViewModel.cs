using AppointmentManager.Models;
using FluentValidation;
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
        private readonly IAppNavigation _navigation;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IDisplay _display;
        private readonly ILoadingFactory _loadingFactory;
        private readonly IValidationFactory _validationFactory;
        public PersonalInformationViewModel(
            IApiClientFactory apiClientFactory,
            IValidationFactory validationFactory,
            IDisplay display,
            IAppNavigation navigation,
            ILoadingFactory loadingFactory)
        {
            _apiClientFactory = apiClientFactory;
            _navigation = navigation;
            _loadingFactory = loadingFactory;
            _validationFactory = validationFactory;
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
        private bool PersonalInformationValidate()
        {
            return _validationFactory.Create<PersonalInformationViewModel>("PersonalInformationForm")
                .AddRule(n => n.Name, n => n
                .NotEmpty()
                .WithMessage("Ingrese un nombre"))
                .AddRule(a => a.Apellido, a => a
                .NotEmpty()
                .WithMessage("Ingrese un Apellido"))
                .AddRule(t => t.TypeDocument, t => t
                .NotEmpty()
                .WithMessage("Seleccione un tipo de documento"))
                .AddRule(d => d.Document, d => d
                .NotEmpty()
                .WithMessage("Ingrese su documento"))
                .AddRule(f => f.Telefono, f => f
                .NotEmpty()
                .WithMessage("Ingrese su telefono"))
                .AddRule(j => j.Address, j => j
                .NotEmpty()
                .WithMessage("Ingrese su direccion"))
                .Validate();

        }
        public async void Confirm()
        {
            if (PersonalInformationValidate())
            { 
            
                Model.FirstName = Name;
                Model.LastName = Apellido;
                Model.TypeDocument = TypeDocument.Type;
                Model.Document = Document;
                Model.PhoneNumber = Telefono;
                Model.Address = Address;
                using (await _loadingFactory.ShowAsync("Subiendo datos", "Espera un momento estamos registrando los datos"))
                using (var client = _apiClientFactory.CreateClient())
                {
                    var result = await client
                        .AppendPath("account")
                        .AddJsonBody(Model)
                        .PostAsAsync<UserModel>();

                    if (result)
                    {
                        await _navigation
                           .GoToAsync("//Main")
                           .NotifyAsync(result.Value);
                    }
                    else
                    {
                        //mostrar mensaje de error
                    }
                }
            }
            
        }

        public void OnNotify(SignUpModel model)
        {
            Model = model;
        }

        #endregion

    }
}
