using AppointmentManager.Models;
using AppointmentManager.Views.Access;
using AppointmentManager.Views.Pets;
using AppointmentManager.Views.Register;
using Netcos.Extensions.Http;
using Netcos.IO.IsolatedStorage;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels
{
    public class MainViewModel : ViewModelBase, INavigated, INotify<UserModel>
    {
        private readonly ISecureStorage _storage;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IDisplay _display;
        private readonly IAppNavigation _navigation;
        private readonly ILoadingFactory _loadingFactory;
        private bool isRefresh;
        private ObservableCollection<NewApointmentModel> appointment;

        public const string user = "UserLoginKey";

        public MainViewModel(
            ISecureStorage storage,
            IApiClientFactory apiClientFactory,
             IDisplay display,
             ILoadingFactory loadingFactory,
            IAppNavigation navigation)
        {
            _storage = storage;
            _apiClientFactory = apiClientFactory;
            _navigation = navigation;
            _loadingFactory = loadingFactory;
            _display = display;
        }




        #region Properties
        public ObservableCollection<NewApointmentModel> Appointment { get => appointment; set => SetProperty(ref appointment, value); }
        public bool IsRefresh { get => isRefresh; set => SetProperty(ref isRefresh, value); }
        #endregion

        #region Commands
        public ICommand Modify => new Command(NavegateToModifyAnimalInformation);
        public ICommand RefreshCommand => new Command(OnNavigated);
        public ICommand AppointmentEditCommand => new Command<NewApointmentModel>(AppointmentEdit);
        public ICommand AppointmentDeleteCommand => new Command<NewApointmentModel>(DeleteAppointment);


        #endregion

        #region Methodos
        private async void AppointmentEdit(NewApointmentModel newApointment)
        {
            await _navigation
                .NavigateToAsync<NewAppoinmentView>()
                .NotifyAsync(newApointment);
        }
        private async void DeleteAppointment(NewApointmentModel newApointment)
        {
            var result = await _display.ConfirmAsync("Eliminar la reserva!", $"Esta seguro de eliminar la reserva");
            if (result)
            {
                using (await _loadingFactory.ShowAsync("Registrando datos", "Espera un momento estamos registrando la reserva"))
                using (var client = _apiClientFactory.CreateClient())
                {
                    var resultPet = await client
                        .AppendPath("Appointment/delete")
                        .AddJsonBody(newApointment)
                        .PostAsync();
                    if (!resultPet)
                        await _display.AlertAsync("Registro de reserva", resultPet.ErrorMessage);
                    else
                        await _navigation.BackAsync();
                }
            }
        }
        private async void NavegateToModifyAnimalInformation()
        {
            await _navigation.NavigateToAsync<AnimalInformationView>();
        }

        public async void OnNotify(UserModel parameter)
        {
            await _storage.SetValueAsync(user, parameter);
            LoadData(parameter.Id);
        }

        public async void OnNavigated()
        {
            var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
            if (user != null)
            {
                Title = $"Hola, {user.FirstName}";
                LoadData(user.Id);
            }
        }

        private async void LoadData(string id)
        {
            using (var client = _apiClientFactory.CreateClient())
            {
                IsRefresh = true;
                var result = await client
                    .AppendPath($"appointment/{id}")
                    .AddQueryParameter("status", AppointmentStatus.PENDING)
                    .GetAsAsync<List<NewApointmentModel>>();
                if (result)
                {
                    Appointment = new ObservableCollection<NewApointmentModel>(result.Value);
                }
                IsRefresh = false;
            }
        }

        #endregion
    }
}
