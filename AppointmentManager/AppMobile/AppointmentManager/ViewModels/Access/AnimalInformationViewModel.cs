using Netcos.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppointmentManager.ViewModels.Access
{
    class AnimalInformationViewModel : ViewModelBase, INavigated
    {
        private ObservableCollection<string> genders;
        private string gender;
        private string animalName;
        private string colorAnimal;
        private string particularSigns;
        private string breeds;

        public AnimalInformationViewModel()
        {
        }

        #region Properties
        public string AnimalName { get => animalName; set => SetProperty(ref animalName, value); }
        //public Animal { get; set; }
        public ObservableCollection<string> Genders { get => genders; set => SetProperty(ref genders, value); }
        public string Gender { get => gender; set => SetProperty(ref gender, value); }
        #endregion
        public string ColorAnimal { get => colorAnimal; set => SetProperty(ref colorAnimal, value); }
        public string ParticularSigns { get => particularSigns; set => SetProperty(ref particularSigns, value); }
        public string Breeds { get => breeds; set => SetProperty(ref breeds, value); }
        #region Commands
        public ICommand Confirmar => new Command(Confirm);
        #endregion

        #region Methodos
        public void OnNavigated()
        {
            Genders = new ObservableCollection<string>(new string[] { "MACHO", "HEMBRA" });
        }
        public void Confirm()
        {

        }

        #endregion
    }


}
