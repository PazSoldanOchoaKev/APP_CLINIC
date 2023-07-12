using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Models
{
    public class AppointmentModel : Appointment
    {
        public string TypeProcedureName { get; set; }
        public string DoctorName { get; set; }
        public string ClientName { get; set; }
    }
}
