using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Netcos.Data;

namespace App.Domain.Entities
{
    public class Users : Entity<int>
    {
        [Required]
        [MaxLength(200)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(200)]
        public string LastName { get; set; }
        [Required]
        public int AccessId { get; set; }

        [ForeignKey(nameof(AccessId))]
        public Access Access { get; set; }
    }
}
