using AppointmentManager.Models;
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

namespace AppointmentManager.ViewModels.Register
{
    public class HistoryAppointmentViewModel : ViewModelBase, INavigated
    {

        private readonly IAppNavigation _navigation;
        private bool isRefresh;
        private readonly IDisplay _display;
        private readonly ISecureStorage _storage;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly ILoadingFactory _loadingFactory;
        private ObservableCollection<NewApointmentModel> appointment;

        public HistoryAppointmentViewModel(
            ISecureStorage storage,
            IApiClientFactory apiClientFactory,
            IDisplay display,
            ILoadingFactory loadingFactory,
            IAppNavigation navigation
            )
        {
            _storage = storage;
            _apiClientFactory = apiClientFactory;
            _navigation = navigation;
            _apiClientFactory = apiClientFactory;
            _display = display;
        }


        #region Properties

        public ObservableCollection<NewApointmentModel> Appointment { get => appointment; set => SetProperty (ref appointment, value); }
        public bool IsRefresh { get => isRefresh; set => SetProperty(ref isRefresh, value); }
        #endregion

        #region Commands
        //public ICommand AppointmentDeleteCommand => new Command<NewApointmentModel>(DeleteAppointment);

        #endregion

        #region Methodos

       /* private async void DeleteAppointment(NewApointmentModel newApointment)
        {
            var result = await _display.ConfirmAsync("Eliminar la reserva!", $"Esta seguro de eliminar la reserva");
            if (result)
            {
                using (await _loadingFactory.ShowAsync("Registrando datos", "Espera un momento estamos registrando la reserva"))
                using (var client = _apiClientFactory.CreateClient())
                {
                    var resultPet = await client
                        .AppendPath("Appointment/delete")
                        .AddJsonBody(appointment)
                        .PostAsync();
                    if (!resultPet)
                        await _display.AlertAsync("Registro de reserva", resultPet.ErrorMessage);
                    else
                        await _navigation.BackAsync();
                }
            }
        }*/

        public async void OnNavigated()
        {
            var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
            using (var client = _apiClientFactory.CreateClient())
            {
                IsRefresh = true;
                var result = await client
                    .AppendPath($"appointment/{user.Id}")
                    .AddQueryParameter("status", AppointmentStatus.DONE)
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
