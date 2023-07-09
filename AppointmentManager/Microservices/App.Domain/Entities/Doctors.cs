using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netcos.Data;

namespace App.Domain.Entities
{
    public class Doctors : Entity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string ProcedureTypeId { get; set; }
        [Required]
        public int StartHour { get; set; }
        [Required]
        public int CountHour { get; set; }

        [ForeignKey(nameof(ProcedureTypeId))]
        public ProcedureTypes ProcedureTypes { get; set; }
    }
}

