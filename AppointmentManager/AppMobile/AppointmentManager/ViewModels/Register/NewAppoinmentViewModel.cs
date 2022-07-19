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
using FluentValidation;
using Xamarin.Essentials;

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
        private ObservableCollection<string> hours;
        private int selectIndex;
        private readonly ILoadingFactory _loadingFactory;
        private readonly IApiClientFactory _apiClientFactory;
        private readonly IAppNavigation _navigation;
        private readonly ISecureStorage _storage;
        private readonly IDisplay _display;
        private readonly IValidationFactory _validationFactory;

        public NewAppoinmentViewModel(
            IAppNavigation navigation,
            ILoadingFactory loadingFactory,
            IValidationFactory validationFactory,
            IDisplay display,
            IApiClientFactory apiClientFactory,
            ISecureStorage storage)
        {
            _navigation = navigation;
            _loadingFactory = loadingFactory;
            _apiClientFactory = apiClientFactory;
            _storage = storage;
            _display = display;
            _validationFactory = validationFactory;
            dateAppointment = DateTime.Now;
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
        public int SelectIndex { get => selectIndex; set => SetProperty(ref selectIndex, value); }

        #endregion

        #region Commands

        public ICommand Reservar => new Command(Reserva);

        #endregion

        #region Methodos

        public async void OnNavigated()
        {
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
                await GetAvailableHoursAsync(DateTime.Now);
            }
        }

        private async void OnDateChanged(DateTime date)
        {
            Hour = null;
            using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estmos obteniendo los horarios disponibles!"))
            {
                await GetAvailableHoursAsync(date);
            }
        }
 
        private async Task GetAvailableHoursAsync(DateTime date)
        {
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("appointment")
                    .AppendPath("avaibalehours")
                    .AddQueryParameter("date", date.ToString("MM/dd/yyyy"))
                    .GetAsAsync<List<string>>();
                if (result)
                    Hours = new ObservableCollection<string>(result.Value);
                else
                    await _display.AlertAsync("Listando datos", "Ocurrio un problema al obtener los horarios");
            }
        }

        private bool Validate()
        {
            return _validationFactory.Create<NewAppoinmentViewModel>("NewAppoinmentForm")
                .AddRule(n => n.Pet, n => n
                .NotEmpty()
                .WithMessage("Selecciona una mascota"))
                .AddRule(p => p.TypeProcedure, p => p
                .NotEmpty()
                .WithMessage("Selecciona un tipo de procedimiento"))
                .AddRule(d => d.DateAppointment, d => d
                .NotEmpty()
                .WithMessage("Selecciona un dia de cita"))
                .AddRule(h => h.Hour, h => h
                .NotEmpty()
                .WithMessage("Seleccione un horario"))
                .AddRule(s => s.Size, s => s
                .NotEmpty()
                .WithMessage("Selecciona un tamaño"))
                .Validate();

        }

        public async void Reserva()
        {
            if (Validate())
            {
                var model = new NewApointmentModel();
                model.Hour = DateTime.ParseExact(Hour, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                model.ListSizes = Size.Type;
                model.PetId = Pet.Id;
                model.TypeProcedures = TypeProcedure.Type;
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
        }
        #endregion
    }
}
