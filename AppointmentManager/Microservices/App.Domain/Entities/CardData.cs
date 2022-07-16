using Netcos.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App.Domain.Entities
{
    public class CardData : Entity
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public DateTime DateExpires { get; set; }
        [Required]
        public string CVV { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public Users Users { get; set; }


    }
}
