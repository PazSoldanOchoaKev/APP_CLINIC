using AppointmentManager.Models;
using FluentValidation;
using Netcos.Extensions.Http;
using Netcos.IO.IsolatedStorage;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using System.Linq;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    public class PersonalInformationViewModel : ViewModelBase, INavigated, INotify<SignUpModel>, INotify<UserModel>
    {
        private string name;
        private string apellido;
        private ObservableCollection<DocumentTypeModel> typeDocuments;
        private DocumentTypeModel typeDocument;
        private string document;
        private string telefono;
        private string address;
        private string mask;
        private readonly IAppNavigation _navigation;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IDisplay _display;
        private readonly ILoadingFactory _loadingFactory;
        private readonly IValidationFactory _validationFactory;
        private readonly ISecureStorage _storage;
        public PersonalInformationViewModel(
            IApiClientFactory apiClientFactory,
            IValidationFactory validationFactory,
            IDisplay display,
            IAppNavigation navigation,
            ILoadingFactory loadingFactory,
            ISecureStorage storage)
        {
            _apiClientFactory = apiClientFactory;
            _navigation = navigation;
            _loadingFactory = loadingFactory;
            _validationFactory = validationFactory;
            _storage = storage;
            _display = display;
        }

        #region Properties
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Apellido { get => apellido; set => SetProperty(ref apellido, value); }
        public ObservableCollection<DocumentTypeModel> TypeDocuments { get => typeDocuments; set => SetProperty(ref typeDocuments, value); }
        public DocumentTypeModel TypeDocument { get => typeDocument; set => SetProperty(ref typeDocument, value, onChanged: OnDocumentTypeChanged); }
        public string Document { get => document; set => SetProperty(ref document, value); }
        public string Telefono { get => telefono; set => SetProperty(ref telefono, value); }
        public string Address { get => address; set => SetProperty(ref address, value); }
        public string Mask { get => mask; set => SetProperty(ref mask, value); }

        public bool IsEdit { get; set; }

        private SignUpModel Model { get; set; }
        private string Id { get; set; }
        private string AccessId { get; set; }

        #endregion

        #region Commands
        public ICommand Confirmar => new Command(Confirm);

        #endregion

        #region Methodos

        public void OnDocumentTypeChanged(DocumentTypeModel model)
        {
            if (model.Type == DocumentType.DNI)
            {
                Mask = "00000000";
            }
            else
            {
                Mask = "";
            }

        }

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
                .WithMessage("Ingrese su documento")
                .NotEqual(p => "00000000")
                .When(p => p.TypeDocument.Type == DocumentType.DNI)
                .WithMessage("Ingrese un número de DNI valido"))
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
                Model.DocumentType = TypeDocument.Type;
                Model.Document = Document;
                Model.PhoneNumber = Telefono;
                Model.Address = Address;
                if (IsEdit)
                {
                    Model.AccessId = AccessId;
                    Model.Id = Id;
                }
                var methodUrl = IsEdit ? "account/edit" : "account";
                using (await _loadingFactory.ShowAsync("Subiendo datos", "Espera un momento estamos registrando los datos"))
                using (var client = _apiClientFactory.CreateClient())
                {
                    var result = await client
                        .AppendPath(methodUrl)
                        .AddJsonBody(Model)
                        .PostAsAsync<UserModel>();

                    if (result)
                    {
                        if (IsEdit)
                        {
                            await _storage.RemoveAsync(MainViewModel.user);
                            await _storage.SetValueAsync(MainViewModel.user, result.Value);
                            await _navigation.BackAsync();
                        }
                        else
                        {
                            await _navigation
                               .GoToAsync("//Main")
                               .NotifyAsync(result.Value);
                        }
                    }
                    else
                    {
                        await _display.AlertAsync("Error!", result.ErrorMessage);
                        //mostrar mensaje de error
                    }
                }
            }

        }

        public void OnNotify(SignUpModel model)
        {
            Title = "Registrarse";
            Model = model;
        }

        public void OnNotify(UserModel user)
        {
            Title = "Modificar Datos";
            IsEdit = true;
            Model = new SignUpModel();
            Name = user.FirstName;
            Apellido = user.LastName;
            TypeDocument = TypeDocuments.FirstOrDefault(d => d.Type == user.DocumentType);
            Document = user.Document;
            Telefono = user.PhoneNumber;
            Address = user.Address;
            AccessId = user.AccessId;
            Id = user.Id;
        }

        #endregion

    }
}
