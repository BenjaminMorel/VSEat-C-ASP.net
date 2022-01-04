using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using DTO;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class StaffToDisplay
    {
        public int IdDeliveryStaff { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string RegionName { get; set; }

        public int IdDeliveryStaffType { get; set; }

        public string DeliveryStaffType { get; set; }

        public StaffToDisplay(string FirstName, string LastName, string PhoneNumber, string RegionName, int IdDeliveryStaffType, string DeliveryStaffType)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.RegionName = RegionName;
            this.IdDeliveryStaffType = IdDeliveryStaffType;
            this.DeliveryStaffType = DeliveryStaffType; 
        }
    }
}
