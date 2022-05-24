using App.Domain.Enums;
using Netcos.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Domain.Entities
{
    public class Pets : Entity
    {
        [Required]
        [MaxLength(100)]
        public string PetName { get; set; }
        [Required]
        public string AninalSpecie  { get; set; }
        [Required]
        public Gender Gender { get; set; }

        [Required]
        [MaxLength(100)]
        public string Color { get; set; }

        [MaxLength(200)]
        public string ParticularSigns { get; set; }

        [Required]
        [MaxLength(100)]
        public string Breed { get; set; }

        public byte[] Photo { get; set; }
    }
}
