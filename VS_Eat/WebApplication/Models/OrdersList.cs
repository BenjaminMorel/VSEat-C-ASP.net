using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTO;

namespace WebApplication.Models
{
    public class OrdersList
    {
        public int OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string DeliveryAddress { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Postcode { get; set; }

        public string City { get; set; }

        public double TotalPrice { get; set; }

        public string OrderStatus { get; set; }
    }
}
