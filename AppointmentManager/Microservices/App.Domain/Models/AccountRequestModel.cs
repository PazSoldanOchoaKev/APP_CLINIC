using System;
using App.Domain.Entities;
using App.Domain.Enums;

namespace App.Domain.Models
{
    public class AccountRequestModel : Users
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

