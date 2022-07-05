using Netcos.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AppointmentManager.Views.Access;
using System.Text;
using Netcos.Extensions.Http;
using Netcos.Xamarin.Forms;
using Netcos.IO.IsolatedStorage;
using AppointmentManager.Models;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.User
{
    public class ProfileViewModel : ViewModelBase, INavigated
    {
        private readonly IAppNavigation _navigation;
        private readonly ISecureStorage _storage;
        private readonly IDisplay _display;
        private readonly IApiClientFactory _apiClientFactory;
        private ObservableCollection<UserModel> user;
        private bool isRefresh;
        private UserModel model;

        public ProfileViewModel(
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


        #region Propertoes
        public ObservableCollection<UserModel> User { get => user; set => SetProperty(ref user, value); }
        public bool IsRefresh { get => isRefresh; set => SetProperty(ref isRefresh, value); }
        public UserModel Model { get => model; set => SetProperty(ref model, value); }

        #endregion

        #region Commands

        public ICommand ModifyCommand => new Command(Modify);

        #endregion

        #region Method

        public async void OnNavigated()
        {
            Model = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
        }

        public async void Modify()
        {
            await _navigation
                .NavigateToAsync<PersonalInformationView>()
                .NotifyAsync(Model);
        }

        #endregion

    }
}
