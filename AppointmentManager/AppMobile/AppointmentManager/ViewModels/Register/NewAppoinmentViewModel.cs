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
        private ObservableCollection<string> typeProcedures;
        private string typeProcedure;
        private ObservableCollection<string> listSizes;
        private string listSize;
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
        public ObservableCollection<string> TypeProcedures { get => typeProcedures; set => SetProperty(ref typeProcedures, value); }
        public string TypeProcedure { get => typeProcedure; set => SetProperty(ref typeProcedure, value); }
        public DateTime DateAppointment { get => dateAppointment; set => SetProperty(ref dateAppointment, value); }
        public TimeSpan Hour { get => hour; set => SetProperty(ref hour, value); }
        public ObservableCollection<string> ListSizes { get => listSizes; set => SetProperty(ref listSizes, value); }
        public string ListSize { get => listSize; set => SetProperty(ref listSize, value); }
        private SignUpModel Model { get; set; }
        #endregion

        #region Commands
        public ICommand Reservar => new Command(Reserva);
        #endregion

        #region Methodos
        public async void OnNavigated()
        {
            TypeProcedures = new ObservableCollection<string>(new string[] { "BAÑO MEDICADO", "BAÑO Y CORTE", "CORTE DE UÑAS" });
            ListSizes = new ObservableCollection<string>(new string[] { " ANIMAL PEQUEÑO(30)", "ANIMAL MEDIANOS (40)", "ANIMAL GRANDES(45)" });

            var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
            using (await _loadingFactory.ShowAsync("Listando datos","Espere un momento estmos obteniendo los datos"))
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath($"pet/{user.Id}")
                    .GetAsAsync<List<AnimalInformationModel>>();
                if (result)
                {
                    Pets = new ObservableCollection<AnimalInformationModel>(result.Value);
                }

            }

        }

        public async void Reserva()
        {
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
        #endregion
    }
}
