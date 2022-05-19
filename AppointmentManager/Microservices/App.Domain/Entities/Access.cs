using System;
using System.ComponentModel.DataAnnotations;
using Netcos.Data;

namespace App.Domain.Entities
{
    public class Access : Entity<int>
    {
        [Required]
        [MaxLength(100)]
        public string User { get; set; }
        [Required]
        [MaxLength(200)]
        public string Password { get; set; }
    }
}
