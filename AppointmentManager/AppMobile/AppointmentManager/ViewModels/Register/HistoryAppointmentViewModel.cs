using AppointmentManager.Models;
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

        public HistoryAppointmentViewModel(
            IAppNavigation navigation
            )
        {
            _navigation = navigation;
        }


        #region Properties
        //public ObservableCollection<AnimalInformationModel> Pets { get => pets; set => SetProperty(ref pets, value); }
        public bool IsRefresh { get => isRefresh; set => SetProperty(ref isRefresh, value); }
        #endregion

        #region Commands

        #endregion

        #region Methodos
        public void OnNavigated()
        {
        }
        #endregion
    }
}
