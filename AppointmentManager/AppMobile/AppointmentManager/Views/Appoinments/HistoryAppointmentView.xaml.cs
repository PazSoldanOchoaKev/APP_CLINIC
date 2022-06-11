using AppointmentManager.Resources.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppointmentManager.Views.Appoinments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryAppointmentView : PageBase
    {
        public HistoryAppointmentView()
        {
            InitializeComponent();
        }
    }
}