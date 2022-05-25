using AppointmentManager.Models;
using AppointmentManager.Views.Access;
using AppointmentManager.Views.Pets;
using Netcos.Extensions.Http;
using Netcos.IO.IsolatedStorage;
using Netcos.Mvvm;
using Netcos.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Pets
{
    public class PetsViewModel : ViewModelBase, INavigated
    {
        private readonly IAppNavigation _navigation;
        private readonly ISecureStorage _storage;
        private readonly IDisplay _display;
        private readonly IApiClientFactory _apiClientFactory;
        private ObservableCollection<AnimalInformationModel> pets;
        private bool isRefresh;

        public PetsViewModel(
            ISecureStorage storage,
            IDisplay display,
            IAppNavigation navigation,
            IApiClientFactory apiClientFactory)
        {
            _navigation = navigation;
            _storage = storage;
            _display = display;
            _apiClientFactory = apiClientFactory;
        }
        #region Properties
        public ObservableCollection<AnimalInformationModel> Pets { get => pets; set => SetProperty(ref pets, value); }
        public bool IsRefresh { get => isRefresh; set => SetProperty(ref isRefresh, value); }
        #endregion


        #region Commands

        public ICommand Confirmar => new Command(NavegateToRegistration);
        public ICommand RefreshCommand => new Command(OnNavigated);

        #endregion

        #region Methodos

        private async void NavegateToRegistration()
        {
            await _navigation.NavigateToAsync<AnimalInformationView>();
        }

        public async void OnNavigated()
        {
            var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
            using (var client = _apiClientFactory.CreateClient())
            {
                IsRefresh = true;
                var result = await client
                    .AppendPath($"pets/{user.Id}")
                    .GetAsAsync<List<AnimalInformationModel>>();
                if (result)
                {
                    Pets = new ObservableCollection<AnimalInformationModel>(result.Value);
                }
                IsRefresh = false;
            }
        }

        #endregion
    }
}