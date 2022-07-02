using AppointmentManager.Models;
using FluentValidation;
using Netcos;
using Netcos.Extensions.Http;
using Netcos.IO.IsolatedStorage;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Pets
{
    public class ModifyAnimalInformationViewModel: ViewModelBase, INavigated
    {
        private ObservableCollection<GenderTypeModel> genders;
        private GenderTypeModel gender;
        private string animalName;
        private string colorAnimal;
        private string particularSigns;
        private string breeds;
        private string animalSpecie;
        private ImageSource image;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IDisplay _display;
        private readonly ISecureStorage _storage;
        private readonly IAppNavigation _navigation;
        private readonly ILoadingFactory _loadingFactory;
        private readonly IValidationFactory _validationFactory;
        public ModifyAnimalInformationViewModel(
            IApiClientFactory apiClientFactory,
            IValidationFactory validationFactory,
            ISecureStorage storage,
            IAppNavigation navigation,
            IDisplay display,
            ILoadingFactory loadingFactory)
        {
            _apiClientFactory = apiClientFactory;
            _storage = storage;
            _display = display;
            _navigation = navigation;
            _loadingFactory = loadingFactory;
            _validationFactory = validationFactory;
        }

        #region Properties
         public string AnimalName { get => animalName; set => SetProperty(ref animalName, value); }
        public string AnimalSpecie { get => animalSpecie; set => SetProperty(ref animalSpecie, value); }
        public ObservableCollection<GenderTypeModel> Genders { get => genders; set => SetProperty(ref genders, value); }
        public GenderTypeModel Gender { get => gender; set => SetProperty(ref gender, value); }
        #endregion
        public string ColorAnimal { get => colorAnimal; set => SetProperty(ref colorAnimal, value); }
        public string ParticularSigns { get => particularSigns; set => SetProperty(ref particularSigns, value); }
        public string Breeds { get => breeds; set => SetProperty(ref breeds, value); }
        public ImageSource Image { get => image; set => SetProperty(ref image, value); }

        public byte[] Photo { get; set; }

        #region Commands
        public ICommand Confirmar => new Command(Confirm);
        public ICommand AddPhotoCommand => new Command(AddPhoto);
        #endregion

        #region Method 
        public void OnNavigated()
        {
            Genders = new ObservableCollection<GenderTypeModel>(new GenderTypeModel[]
              {
                new GenderTypeModel { Type = GenderType.MACHO, Name = "MACHO" },
                new GenderTypeModel { Type = GenderType.HEMBRA, Name = "HEMBRA"}
              });
        }
        private async void AddPhoto(object obj)
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Seleccionar imagen",
            });
            if (photo != null)
            {
                Photo = (await photo.OpenReadAsync()).ToArray();
                Image = ImageSource.FromStream(() => new MemoryStream(Photo));
            }
        }
        private bool AnimalInformationValidate()
        {
            return _validationFactory.Create<ModifyAnimalInformationViewModel>("AnimalInformationForm")
                .AddRule(n => n.AnimalName, n => n
                .NotEmpty()
                .WithMessage("Ingrese un nombre de mascota"))
                .AddRule(s => s.AnimalSpecie, s => s
                .NotEmpty()
                .WithMessage("Ingrese la especie del animal"))
                .AddRule(g => g.Gender, g => g
                .NotEmpty()
                .WithMessage("Seleccione el sexo"))
                .AddRule(c => c.ColorAnimal, c => c
                .NotEmpty()
                .WithMessage("Ingresar el color de la mascota"))
                .AddRule(p => p.ParticularSigns, p => p
                .NotEmpty()
                .WithMessage("Ingresar Señas particulares"))
                .AddRule(b => b.Breeds, b => b
                .NotEmpty()
                .WithMessage("Ingresar raza de la mascota"))
                .Validate();

        }
        public async void Confirm()
        {
            if (AnimalInformationValidate())
            {
                var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
                var model = new AnimalInformationModel
                {
                    AninalSpecie = AnimalSpecie,
                    Breed = Breeds,
                    Color = ColorAnimal,
                    Gender = Gender.Type,
                    ParticularSigns = ParticularSigns,
                    PetName = AnimalName,
                    Photo = Photo,
                    UserId = user.Id
                };
                using (await _loadingFactory.ShowAsync("Registrando datos", "Espera un momento estamos registrando los datos"))
                using (var client = _apiClientFactory.CreateClient())
                {
                    var result = await client
                        .AppendPath("pets")
                        .AddJsonBody(model)
                        .PostAsync();
                    if (!result)
                    {
                        await _display.AlertAsync("Registro Mascota", result.ErrorMessage);
                    }
                    else
                    {
                        await _navigation.BackAsync();
                    }
                }
            }


        }
        #endregion
    }
}
