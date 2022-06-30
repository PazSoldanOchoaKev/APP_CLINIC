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
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;

namespace AppointmentManager.ViewModels.Register
{
    public class NewAppoinmentViewModel : ViewModelBase, INavigated
    {
        private ObservableCollection<TypeProceduresModel> typeProceduresModels;
        private TypeProceduresModel typeProcedure;
        private ObservableCollection<ListSizesModel> listSizes;
        private ListSizesModel size;
        private string hour;
        private DateTime dateAppointment;
        private ObservableCollection<AnimalInformationModel> pets;
        private AnimalInformationModel pet;
        private DateTime minimun;
        private ObservableCollection<string> hours;
        private readonly ILoadingFactory _loadingFactory;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IAppNavigation _navigation;
        private readonly ISecureStorage _storage;
        private readonly IDisplay _display;

        public NewAppoinmentViewModel(
            IAppNavigation navigation,
            ILoadingFactory loadingFactory,
            IDisplay display,
            IApiClientFactory apiClientFactory,
            ISecureStorage storage)
        {
            _navigation = navigation;
            _loadingFactory = loadingFactory;
            _apiClientFactory = apiClientFactory;
            _storage = storage;
            _display = display;
        }

        #region Properties
        public ObservableCollection<AnimalInformationModel> Pets { get => pets; set => SetProperty(ref pets, value); }
        public AnimalInformationModel Pet { get => pet; set => SetProperty(ref pet, value); }
        public ObservableCollection<TypeProceduresModel> TypeProceduresModels { get => typeProceduresModels; set => SetProperty(ref typeProceduresModels, value); }
        public TypeProceduresModel TypeProcedure { get => typeProcedure; set => SetProperty(ref typeProcedure, value); }
        public DateTime DateAppointment { get => dateAppointment; set => SetProperty(ref dateAppointment, value, onChanged: OnDateChanged); }
        public string Hour { get => hour; set => SetProperty(ref hour, value); }
        public ObservableCollection<string> Hours { get => hours; set => SetProperty(ref hours, value); }
        public ObservableCollection<ListSizesModel> ListSizes { get => listSizes; set => SetProperty(ref listSizes, value); }
        public ListSizesModel Size { get => size; set => SetProperty(ref size, value); }
        public DateTime Minimun { get => minimun; set => SetProperty(ref minimun, value); }


        #endregion

        #region Commands
        public ICommand Reservar => new Command(Reserva);
        #endregion

        #region Methodos
        public async void OnNavigated()
        {
            Minimun = DateTime.Now;
            TypeProceduresModels = new ObservableCollection<TypeProceduresModel>(new TypeProceduresModel[] {
                new TypeProceduresModel { Type = AppointmentManager.TypeProcedures.BAÑO_MEDICADO, Name = "BAÑO MEDICADO" },
                new TypeProceduresModel { Type = AppointmentManager.TypeProcedures.BAÑO_Y_CORTE, Name = "BAÑO Y CORTE" },
                new TypeProceduresModel { Type = AppointmentManager.TypeProcedures.CORTE_DE_UÑAS, Name = "CORTE DE UÑAS" }
            });


            ListSizes = new ObservableCollection<ListSizesModel>(new ListSizesModel[]
            {
                new ListSizesModel{ Type = AppointmentManager.ListSizes.ANIMAL_PEQUEÑO_30, Name= "ANIMAL PEQUEÑO(30)"},
                new ListSizesModel{ Type = AppointmentManager.ListSizes.ANIMAL_MEDIANOS_40, Name= "ANIMAL MEDIANOS(40)" },
                new ListSizesModel{ Type = AppointmentManager.ListSizes.ANIMAL_GRANDES_45, Name= "ANIMAL GRANDES(45)" }

            });

            Hours = new ObservableCollection<string>();

            var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
            using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estmos obteniendo los datos"))
            {
                using (var client = _apiClientFactory.CreateClient())
                {
                    var result = await client
                        .AppendPath($"pets/{user.Id}")
                        .GetAsAsync<List<AnimalInformationModel>>();
                    if (result)
                    {
                        Pets = new ObservableCollection<AnimalInformationModel>(result.Value);
                    }
                }
                await loadAvailableHoursAsync();
            }
        }

        private async Task loadAvailableHoursAsync()
        {
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("appointment")
                    .AppendPath("avaibalehours")
                    .AddQueryParameter("date", DateAppointment.ToShortDateString())
                    .GetAsAsync<List<string>>();
                if (result)
                    Hours = new ObservableCollection<string>(result.Value);
                else
                    await _display.AlertAsync("Listando datos", "Ocurrio un problema al obtener los horarios");
            }

        }

        private async void OnDateChanged(DateTime date)
        {
            using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estmos obteniendo los horarios disponibles!"))
            {
                await loadAvailableHoursAsync();
            }
        }

        public async void Reserva()
        {
            var model = new NewApointmentModel();
            model.Hour = DateTime.ParseExact(Hour, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
            model.sizes = Size.Type;
            model.PetId = Pet.Id;
            model.TypeProcedure = TypeProcedure.Type;
            model.DateAppointment = DateAppointment;
            model.Status = AppointmentStatus.PENDING;

            using (await _loadingFactory.ShowAsync("Subiendo datos", "Espera un momento estamos registrando los datos"))
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("appointment")
                    .AddJsonBody(model)
                    .PostAsync();

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
