using System;
using System.Collections.Generic;
using System.Text;

namespace AppointmentManager.Models
{
    [Serializable]
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Id { get; set; }
    }
}
