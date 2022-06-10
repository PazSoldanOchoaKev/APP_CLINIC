using App.Domain.Enums;
using Netcos.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class Appointment : Entity
    {
        [Required]
        public string PetId { get; set; }
        [Required]
        public TypeProcedures TypeProcedures { get; set; }
        [Required]
        public DateTime DateAppointment { get; set; }
        [Required]
        public TimeSpan Hour { get; set; }
        [Required]
        public ListSizes ListSizes { get; set; }

        [Required]
        public AppoinmentStatus Status { get; set; }
        public string Specificaction { get; set; }

        [ForeignKey(nameof(PetId))]
        public Pets Pets { get; set; }
    }
}
