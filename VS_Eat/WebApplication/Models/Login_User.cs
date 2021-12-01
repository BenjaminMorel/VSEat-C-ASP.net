using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Login_User
    {
		[Required]
		public string Username { get; set; } // from Login

		[Required]
		[EmailAddress]
		public string Password { get; set; } // from Login

		[Required]
		public string FirstName { get; set; } // from user

		[Required]
		public string LastName { get; set; } // from User

		[Required]
		public string PhoneNumber { get; set; } // from user

		[Required]
		public int PostCode { get; set; } // from location


		[Required]
		public string City { get; set; } // from location

		[Required]
		public string Address { get; set; } // from User

	}
}
