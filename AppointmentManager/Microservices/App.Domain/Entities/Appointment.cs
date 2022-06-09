using App.Domain.Enums;
using Netcos.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Entities
{
    public class Appointment : Entity
    {
        public string IdUsuario { get; set; }
        public string IdMascota { get; set; }
        public TypeProcedures TypeProcedures { get; set; }
        public DateTime DateTime{get;set;}
        public ListSizes ListSizes { get; set; }
        public ICollection<Appointment> Appointments { get; set; }

        
    }
}
