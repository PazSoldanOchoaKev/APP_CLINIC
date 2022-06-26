using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Models
{
    public class NewApointmentModel
    {
        public string PetId { get; set; }
        public TypeProcedures TypeProcedure { get; set; }
        public DateTime DateAppointment { get; set; }
        public TimeSpan Hour { get; set; }
        public ListSizes sizes {get;set;}

        public AppointmentStatus Status { get; set; }
       
        public AnimalInformationModel Pets { get; set; }
    }


}
