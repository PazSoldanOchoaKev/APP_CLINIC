using System;
using App.Domain.Enums;

namespace App.Domain.Models
{
	public class AccountRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DocumentType TypeDocument { get; set; }
        public string Document { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}

