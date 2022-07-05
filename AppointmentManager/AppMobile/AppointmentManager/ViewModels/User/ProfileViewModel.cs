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
    public class ProfileViewModel: ViewModelBase, INavigated
    {
        private readonly IAppNavigation _navigation;
        private readonly ISecureStorage _storage;
        private readonly IDisplay _display;
        private readonly IApiClientFactory _apiClientFactory;
        private ObservableCollection<UserModel> user;
        private bool isRefresh;
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


        #endregion

        #region Commands

        public ICommand Modify => new Command<PersonalInformationModel>(InformationEdit);
        #endregion

        #region Method
        private async void PetEdit(PersonalInformationModel user)
        {
            await _navigation
                .NavigateToAsync<AnimalInformationView>()
                .NotifyAsync(user);
        }
        public void OnNavigated()
        {
          
        }
        #endregion

    }
}
