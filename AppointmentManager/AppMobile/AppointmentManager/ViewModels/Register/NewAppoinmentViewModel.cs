using System;
using System.Collections.ObjectModel;
using Netcos.Mvvm;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Register
{
    public class NewAppoinmentViewModel : ViewModelBase, INavigated
    {
        private ObservableCollection<string> typeProcedures;
        private string typeProcedure;
        private ObservableCollection<string> listSizes;
        private string listSize;
        private DateTime dateTime;
        private DateTime dateAppointment;

        public NewAppoinmentViewModel()
        {
        }

        #region Properties
        public ObservableCollection<string> TypeProcedures { get => typeProcedures; set => SetProperty(ref typeProcedures, value); }
        public string TypeProcedure { get => typeProcedure; set => SetProperty(ref typeProcedure, value); }
        public DateTime DateAppointment { get => dateAppointment; set =>SetProperty(ref dateAppointment, value); }
        public DateTime DateTime { get => dateTime; set => SetProperty(ref dateTime, value); }
        public ObservableCollection<string> ListSizes { get => listSizes; set => SetProperty(ref listSizes, value); }
        public string ListSize { get => listSize; set => SetProperty(ref listSize, value); }
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

        public void Reserva()
        {

        }
        #endregion
    }
}
