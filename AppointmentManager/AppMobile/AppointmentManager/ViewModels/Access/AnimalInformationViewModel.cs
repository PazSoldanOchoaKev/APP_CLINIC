using AppointmentManager.Models;
using Netcos.Extensions.Http;
using Netcos.Mvvm;
using System;
using Netcos;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.IO;
using Netcos.Xamarin.Forms;
using Netcos.IO.IsolatedStorage;

namespace AppointmentManager.ViewModels.Access
{
    class AnimalInformationViewModel : ViewModelBase, INavigated
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
        public AnimalInformationViewModel(
            IApiClientFactory apiClientFactory,
            ISecureStorage storage,
            IDisplay display)
        {
            _apiClientFactory = apiClientFactory;
            _storage = storage;
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

        #region Methodos
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

        public async void Confirm()
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
            }

        }

        #endregion
    }


}
