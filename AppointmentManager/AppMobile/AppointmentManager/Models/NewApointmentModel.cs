using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Models
{
    public class NewApointmentModel
    {
        public string UserId { get; set; }
        public string TypeProcedures { get; set; }
        public DateTime DateAppointment { get; set; }
        public TimeSpan Hour { get; set; }
        public string ListSizes {get;set;}
        

    }
}
