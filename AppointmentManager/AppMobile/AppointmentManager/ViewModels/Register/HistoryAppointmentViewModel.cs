using AppointmentManager.Models;
using Netcos.Extensions.Http;
using Netcos.IO.IsolatedStorage;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppointmentManager.ViewModels.Register
{
    public class HistoryAppointmentViewModel : ViewModelBase, INavigated
    {

        private readonly IAppNavigation _navigation;
        private bool isRefresh;
        private readonly ISecureStorage _storage;
        private readonly IApiClientFactory _apiClientFactory;
        private ObservableCollection<NewApointmentModel> appointment;

        public HistoryAppointmentViewModel(
            ISecureStorage storage,
            IApiClientFactory apiClientFactory,
            IAppNavigation navigation
            )
        {
            _storage = storage;
            _apiClientFactory = apiClientFactory;
            _navigation = navigation;
        }


        #region Properties

        public ObservableCollection<NewApointmentModel> Appointment { get => appointment; set => SetProperty (ref appointment, value); }
        public bool IsRefresh { get => isRefresh; set => SetProperty(ref isRefresh, value); }
        #endregion

        #region Commands

        #endregion

        #region Methodos
        public async void OnNavigated()
        {
            var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
            using (var client = _apiClientFactory.CreateClient())
            {
                IsRefresh = true;
                var result = await client
                    .AppendPath($"appointment/{user.Id}")
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
