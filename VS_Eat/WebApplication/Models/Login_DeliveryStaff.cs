﻿using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Login_DeliveryStaff
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } // from Login

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; } // from Login

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        public string ConfirmPassword { get; set; } // from Login

        [Required]
        public string FirstName { get; set; } // from DeliveryStaff

        [Required]
        public string LastName { get; set; } // from DeliveryStaff

        [Required]
        public string PhoneNumber { get; set; } // from DeliveryStaff

        [Required]
        public string Address { get; set; } // from DeliveryStaff

        [Required]
        public int PostCode { get; set; } // from Location

        [Required]
        public string City { get; set; } // from Location

        [Required]
        public string RegionName { get; set; }
        //public List<Region> AllRegions {get; set; } // from Region

	}
}
