using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Models
{
    public class NewApointmentModel
    {
        public string PetId { get; set; }
        public string TypeProcedure { get; set; }
        public DateTime DateAppointment { get; set; }
        public TimeSpan Hour { get; set; }
        public string sizes {get;set;}

        public AppointmentStatus Status { get; set; }
       

    }
}
