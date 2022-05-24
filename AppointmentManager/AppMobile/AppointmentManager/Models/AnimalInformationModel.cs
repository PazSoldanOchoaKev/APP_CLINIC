using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace AppointmentManager.Models
{
    public class AnimalInformationModel
    {
        public string PetName { get; set; }
        public string AninalSpecie { get; set; }
        public GenderType Gender { get; set; }
        public string Color { get; set; }
        public string ParticularSigns { get; set; }
        public string Breed { get; set; }
        public byte[] Photo { get; set; }
        public string UserId { get; set; }

        public ImageSource Image => ImageSource.FromStream(() => new MemoryStream(Photo));
    }
}
