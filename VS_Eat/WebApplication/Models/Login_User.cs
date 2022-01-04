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
		[EmailAddress]
		public string Username { get; set; } // from Login

		[Required]
        [DataType(DataType.Password)]
		[MinLength(6)]
		public string Password { get; set; } // from Login

		[Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; } // from Login

		[Required]
		public string FirstName { get; set; } // from user

		[Required]
		public string LastName { get; set; } // from User

		[Required]
		[MinLength(10)]
		[MaxLength(10)]
		public string PhoneNumber { get; set; } // from user

		[Required]
		public int PostCode { get; set; } // from location

        [Required]
		public string City { get; set; } // from location

		[Required]
		public string Address { get; set; } // from User


		public Login_User(string Username, string Password, string FirstName, string LastName, string PhoneNumber, int PostCode, string City, string Address)
        {
			this.Username = Username;
			this.Password = Password;
			this.FirstName = FirstName;
			this.LastName = LastName;
			this.PhoneNumber = PhoneNumber;
			this.PostCode = PostCode;
			this.City = City;
			this.Address = Address; 
        }
	}
}
