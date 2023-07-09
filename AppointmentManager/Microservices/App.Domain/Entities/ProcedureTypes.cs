using System;
using System.ComponentModel.DataAnnotations;
using Netcos.Data;

namespace App.Domain.Entities
{
    public class ProcedureTypes : Entity
    {
        [Required]
        public string Type { get; set; }
    }
}

