using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using App.Domain.Enums;
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
        [Required]
        public DocumentType DocumentType { get; set; }
        [Required]
        [MaxLength(100)]
        public string Document { get; set; }
        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }


        [ForeignKey(nameof(AccessId))]
        public Access Access { get; set; }
    }
}
