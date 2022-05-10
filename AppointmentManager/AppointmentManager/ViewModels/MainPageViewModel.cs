using System;
using System.Windows.Input;
using AppointmentManager.Models;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels
{
    public class MainPageViewModel
    {
        public MainPageViewModel()
        {
            App = new ContentApp
            {
                Title = "App de Kevin",
                SubTitle = "Detalle de la aplicacion"
            };
        }

        #region Properties

        public ICommand Naigation => new Command(Navegar);
        public ContentApp App;

        #endregion

        #region Methods

        public void Navegar()
        {

        }

        #endregion
    }
}
