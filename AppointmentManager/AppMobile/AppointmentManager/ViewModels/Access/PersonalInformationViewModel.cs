using Netcos.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    public class PersonalInformationViewModel : ViewModelBase, INavigated
    {
        private string name;
        private string apellido;

        public PersonalInformationViewModel()
        {
        }
        #region Properties
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Apellido { get => apellido; set => SetProperty(ref apellido, value); }


        #endregion

        #region Commands
        public ICommand Confirmar => new Command(Confirm);

        #endregion

        #region Methodos
        public void OnNavigated()
        {

        }

        public void Confirm()
        {

        }

        #endregion

    }
}
