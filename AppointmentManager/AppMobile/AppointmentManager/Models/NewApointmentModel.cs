using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Models
{
    public class NewApointmentModel
    {
        public string Id { get; set; }
        public string PetId { get; set; }
        public TypeProcedures TypeProcedures { get; set; }
        public DateTime DateAppointment { get; set; }
        public TimeSpan Hour { get; set; }
        public ListSizes ListSizes { get; set; }

        public AppointmentStatus Status { get; set; }

        public AnimalInformationModel Pets { get; set; }
    }


}
