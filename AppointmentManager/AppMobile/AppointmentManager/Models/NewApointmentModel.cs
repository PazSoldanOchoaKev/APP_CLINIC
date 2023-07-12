using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Models
{
    public class NewApointmentModel
    {
        public string Id { get; set; }
        public string PetId { get; set; }
        public string TypeProcedureId { get; set; }
        public string DoctorId { get; set; }
        public DateTime DateAppointment { get; set; }
        public TimeSpan Hour { get; set; }
        public string HourFormat => DateTime.MinValue.Add(Hour).ToString("hh:mm tt");
        public AppointmentStatus Status { get; set; }
        public AnimalInformationModel Pets { get; set; }
        public string TypeProcedureName { get; set; }
        public string DoctorName { get; set; }
        public string ClientName { get; set; }
    }


}
