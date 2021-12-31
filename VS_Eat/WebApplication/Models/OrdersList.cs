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

        public string RecipientFirstName { get; set; }

        public string RecipientLastName { get; set; }

        public int Postcode { get; set; }

        public string City { get; set; }

        public double TotalPrice { get; set; }

        public int IdOrderStatus { get; set; }

        public string OrderStatus { get; set; }

        public string  RestaurantName { get; set; }
        
        public string RestaurantAddress { get; set; }

        public int RestaurantPostCode { get; set; }
        
        public string RestaurantCity { get; set; }

    }
}
