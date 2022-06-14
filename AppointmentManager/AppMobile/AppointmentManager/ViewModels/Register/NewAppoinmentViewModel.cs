using System;
using System.Collections.ObjectModel;
using Netcos.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;
using Netcos.Xamarin.Forms;
using Netcos.Extensions.Http;
using AppointmentManager.Models;
using Netcos.IO.IsolatedStorage;
using System.Collections.Generic;

namespace AppointmentManager.ViewModels.Register
{
    public class NewAppoinmentViewModel : ViewModelBase, INavigated
    {
        private ObservableCollection<TypeProceduresModel> typeProceduresModels;
        private string typeProcedure;
        private ObservableCollection<ListSizesModel> listSizes;
        private string size;
        private TimeSpan hour;
        private DateTime dateAppointment;
        private ObservableCollection<AnimalInformationModel> pets;
        private AnimalInformationModel pet;
        private readonly ILoadingFactory _loadingFactory;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IAppNavigation _navigation;
        private readonly ISecureStorage _storage;

        public NewAppoinmentViewModel(
            IAppNavigation navigation,
            ILoadingFactory loadingFactory,
            IApiClientFactory apiClientFactory,
            ISecureStorage storage)
        {
            _navigation = navigation;
            _loadingFactory = loadingFactory;
            _apiClientFactory = apiClientFactory;
            _storage = storage;
        }

        #region Properties
        public ObservableCollection<AnimalInformationModel> Pets { get => pets; set => SetProperty(ref pets, value); }
        public AnimalInformationModel Pet { get => pet; set => SetProperty(ref pet, value); }
        public ObservableCollection<TypeProceduresModel> TypeProceduresModels { get => typeProceduresModels; set => SetProperty(ref typeProceduresModels, value); }
        public string TypeProcedure { get => typeProcedure; set => SetProperty(ref typeProcedure, value); }
        public DateTime DateAppointment { get => dateAppointment; set => SetProperty(ref dateAppointment, value); }
        public TimeSpan Hour { get => hour; set => SetProperty(ref hour, value); }
        public ObservableCollection<ListSizesModel> ListSizes { get => listSizes; set => SetProperty(ref listSizes, value); }
        public string Size { get => size; set => SetProperty(ref size, value); }


        #endregion

        #region Commands
        public ICommand Reservar => new Command(Reserva);
        #endregion

        #region Methodos
        public async void OnNavigated()
        {
            TypeProceduresModels = new ObservableCollection<TypeProceduresModel>(new TypeProceduresModel[] {
                new TypeProceduresModel { Type = AppointmentManager.TypeProcedures.BAÑO_MEDICADO, Name = "BAÑO MEDICADO" },
                new TypeProceduresModel { Type = AppointmentManager.TypeProcedures.BAÑO_Y_CORTE, Name = "BAÑO Y CORTE" },
                new TypeProceduresModel { Type = AppointmentManager.TypeProcedures.CORTE_DE_UÑAS, Name = "CORTE DE UÑAS" }
            });


            ListSizes = new ObservableCollection<ListSizesModel>(new ListSizesModel[]
            {
                new ListSizesModel{ Type = AppointmentManager.ListSizes.ANIMAL_PEQUEÑO_30, Name= "ANIMAL PEQUEÑO(30)"},
                new ListSizesModel{ Type = AppointmentManager.ListSizes.ANIMAL_MEDIANOS_40, Name= "ANIMAL MEDIANOS (40)" },
                new ListSizesModel{ Type = AppointmentManager.ListSizes.ANIMAL_GRANDES_45, Name= "ANIMAL GRANDES(45)" }

            });

            var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
            using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estmos obteniendo los datos"))
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath($"pets/{user.Id}")
                    .GetAsAsync<List<AnimalInformationModel>>();
                if (result)
                {
                    Pets = new ObservableCollection<AnimalInformationModel>(result.Value);
                }

            }

        }

        public async void Reserva()
        {
            var model = new NewApointmentModel();
            model.Hour = Hour;
            model.sizes = Size;
            model.PetId = Pet.Id;
            model.TypeProcedure = TypeProcedure;
            model.Status = AppointmentStatus.PENDING;

            using (await _loadingFactory.ShowAsync("Subiendo datos", "Espera un momento estamos registrando los datos"))
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("account")
                    .AddJsonBody(model)
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
        #endregion
    }
}
