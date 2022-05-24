using AppointmentManager.Models;
using Netcos.IO.IsolatedStorage;
using Netcos.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.ViewModels
{
    public class MainViewModel : ViewModelBase, INotify<UserModel>
    {
        private readonly ISecureStorage _storage;

        public const string user = "UserLoginKey";

        public MainViewModel(
            ISecureStorage storage)
        {
            _storage = storage;
        }

        public async void OnNotify(UserModel parameter)
        {
            await _storage.SetValueAsync(user, parameter);
        }
    }
}
