using System;
using System.Collections.ObjectModel;
using Netcos.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;
using Netcos.Xamarin.Forms;
using Netcos.Extensions.Http;
using AppointmentManager.Models;

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
        private readonly ILoadingFactory _loadingFactory;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IAppNavigation _navigation;

        public NewAppoinmentViewModel(
            IAppNavigation navigation,
            ILoadingFactory loadingFactory,
            IApiClientFactory apiClientFactory)
        {
            _navigation = navigation;
            _loadingFactory = loadingFactory;
            _apiClientFactory = apiClientFactory;
        }

        #region Properties

        public ObservableCollection<string> TypeProcedures { get => typeProcedures; set => SetProperty(ref typeProcedures, value); }
        public string TypeProcedure { get => typeProcedure; set => SetProperty(ref typeProcedure, value); }
        public DateTime DateAppointment { get => dateAppointment; set =>SetProperty(ref dateAppointment, value); }
        public TimeSpan Hour { get => hour; set => SetProperty(ref hour, value); }
        public ObservableCollection<string> ListSizes { get => listSizes; set => SetProperty(ref listSizes, value); }
        public string ListSize { get => listSize; set => SetProperty(ref listSize, value); }
        private SignUpModel Model { get; set; }
        #endregion

        #region Commands
        public ICommand Reservar => new Command(Reserva);
        #endregion

        #region Methodos
        public void OnNavigated()
        {
            TypeProcedures = new ObservableCollection<string>(new string[] { "BAÑO MEDICADO", "BAÑO Y CORTE", "CORTE DE UÑAS" });
            ListSizes = new ObservableCollection<string>(new string[] { " ANIMAL PEQUEÑO(30)", "ANIMAL MEDIANOS (40)", "ANIMAL GRANDES(45)" });
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
