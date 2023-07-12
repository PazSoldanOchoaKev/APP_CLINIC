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
    public class NewAppoinmentViewModel : ViewModelBase, INotify, INotify<NewApointmentModel>
    {
        private ObservableCollection<TypeProceduresModel> typeProceduresModels;
        private TypeProceduresModel typeProcedure;
        private ObservableCollection<DoctorsModel> doctors;
        private DoctorsModel doctor;
        private HourModel hour;
        private DateTime dateAppointment;
        private ObservableCollection<AnimalInformationModel> pets;
        private AnimalInformationModel pet;
        private ObservableCollection<HourModel> hours;
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
        public TypeProceduresModel TypeProcedure { get => typeProcedure; set => SetProperty(ref typeProcedure, value, onChanged: OnProcedureChanged); }
        public DateTime DateAppointment { get => dateAppointment; set => SetProperty(ref dateAppointment, value, onChanged: OnDateChanged); }
        public HourModel Hour { get => hour; set => SetProperty(ref hour, value); }
        public ObservableCollection<HourModel> Hours { get => hours; set => SetProperty(ref hours, value); }
        public ObservableCollection<DoctorsModel> Doctors { get => doctors; set => SetProperty(ref doctors, value); }
        public DoctorsModel Doctor { get => doctor; set => SetProperty(ref doctor, value, onChanged: OnDoctorChanged); }

        public int SelectIndex { get => selectIndex; set => SetProperty(ref selectIndex, value); }
        public bool IsEdit { get; set; }
        public bool IsEditLoad { get; set; }
        public string Id { get; set; }
        public TaskCompletionSource<bool> tcs;
        public TaskCompletionSource<bool> tcsDoctors;
        public TaskCompletionSource<bool> tcsHours;
        #endregion

        #region Commands

        public ICommand Reservar => new Command(Reserva);

        #endregion

        #region Methodos

        public async void OnNotify()
        {
            using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estmos obteniendo los datos"))
            {
                await GetClients();
                await GetProcedureTypes();
            }
        }

        private async Task GetClients()
        {
            var user = await _storage.GetValueAsync<UserModel>(MainViewModel.user);
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
        }

        private async void OnDateChanged(DateTime date)
        {
            if (!IsEditLoad)
            {
                Hour = null;
                Hours = null;
                using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estamos obteniendo los horarios disponibles!"))
                {
                    await GetAvailableHoursAsync(Doctor?.Id ?? "", date);
                }
                tcsHours.SetResult(true);
            }
        }

        private async void OnDoctorChanged(DoctorsModel model)
        {
            if (!IsEditLoad)
            {
                Hour = null;
                using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estamos obteniendo los horarios disponibles!"))
                {
                    await GetAvailableHoursAsync(model?.Id ?? "", DateAppointment);
                }
            }
        }

        private async void OnProcedureChanged(TypeProceduresModel model)
        {
            if (!IsEditLoad)
            {
                Doctor = null;
                Hours = null;
                hour = null;
                using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estmos obteniendo los doctores!"))
                {
                    await GetDoctorsByProcedureType(model.Id);
                }
            }
        }

        private async Task GetAvailableHoursAsync(string doctorId, DateTime date)
        {
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("appointment")
                    .AppendPath(doctorId)
                    .AppendPath("avaibalehours")
                    .AddQueryParameter("date", date.ToString("MM/dd/yyyy"))
                    .AddQueryParameter("zone", TimeZoneInfo.Local.Id)
                    .GetAsAsync<List<int>>();
                if (result)
                    Hours = new ObservableCollection<HourModel>(result.Value.Select(h => new HourModel
                    {
                        HourFormat = DateTime.MinValue.AddHours(h).ToString("hh:mm tt"),
                        Hour = TimeSpan.FromHours(h)
                    }));
                else
                    Hours = null;
            }
        }

        private async Task GetProcedureTypes()
        {
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("appointment")
                    .AppendPath("procedures")
                    .GetAsAsync<IEnumerable<TypeProceduresModel>>();
                if (result)
                {
                    TypeProceduresModels = new ObservableCollection<TypeProceduresModel>(result.Value);
                }
            }
        }

        private async Task GetDoctorsByProcedureType(string procedureId)
        {
            using (var client = _apiClientFactory.CreateClient())
            {
                var result = await client
                    .AppendPath("appointment")
                    .AppendPath(procedureId)
                    .AppendPath("doctors")
                    .GetAsAsync<IEnumerable<DoctorsModel>>();
                if (result)
                {
                    Doctors = new ObservableCollection<DoctorsModel>(result.Value);
                }
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
                .AddRule(s => s.Doctor, s => s
                .NotEmpty()
                .WithMessage("Selecciona un tamaño"))
                .Validate();

        }

        public async void Reserva()
        {
            if (Validate())
            {
                var model = new NewApointmentModel();
                model.Hour = Hour.Hour;
                model.TypeProcedureId = TypeProcedure.Id;
                model.PetId = Pet.Id;
                model.DoctorId = Doctor.Id;
                model.DateAppointment = DateAppointment;
                model.Status = AppointmentStatus.PENDING;

                if (IsEdit)
                    model.Id = Id;
                var methodUrl = IsEdit ? "appointment/edit" : "appointment";
                using (await _loadingFactory.ShowAsync("Subiendo datos", "Espera un momento estamos registrando los datos"))
                using (var client = _apiClientFactory.CreateClient())
                {
                    var result = await client
                        .AppendPath(methodUrl)
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

        public async void OnNotify(NewApointmentModel newApointment)
        {
            IsEdit = true;
            IsEditLoad = true;
            using (await _loadingFactory.ShowAsync("Listando datos", "Espere un momento estmos obteniendo los datos"))
            {
                await GetClients();
                Pet = Pets.FirstOrDefault(p => p.Id == newApointment.PetId);
                await GetProcedureTypes();
                TypeProcedure = TypeProceduresModels.FirstOrDefault(tp => tp.Id == newApointment.TypeProcedureId);
                await GetDoctorsByProcedureType(TypeProcedure.Id);
                Doctor = Doctors.FirstOrDefault(d => d.Id == newApointment.DoctorId);
                DateAppointment = newApointment.DateAppointment;
                await GetAvailableHoursAsync(Doctor.Id, DateAppointment);
                Hour = Hours.FirstOrDefault(h => h.Hour == newApointment.Hour);
                Id = newApointment.Id;
            }
            IsEditLoad = false;
        }
        #endregion
    }
}
