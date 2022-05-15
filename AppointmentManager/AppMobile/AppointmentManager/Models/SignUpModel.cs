using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Models
{
    public class SignUpModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Apellido { get; set; }
        public string TypeDocument { get;set; }
        public string Document { get; set; }
        public string Telefono { get; set; }
        public string Address { get; set; }
    }
}
