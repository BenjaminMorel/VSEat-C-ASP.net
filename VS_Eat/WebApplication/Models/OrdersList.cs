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
        public int IdOrder { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string DeliveryAddress { get; set; }

        public string RecipientFirstName { get; set; }

        public string RecipientLastName { get; set; }

        public int Postcode { get; set; }

        public string City { get; set; }

        public float TotalPrice { get; set; }

        public int IdOrderStatus { get; set; }

        public string OrderStatus { get; set; }

        public string RestaurantName { get; set; }
        
        public string RestaurantAddress { get; set; }

        public int RestaurantPostCode { get; set; }
        
        public string RestaurantCity { get; set; }

        public OrdersList(int idOrder, DateTime orderDate, DateTime deliveryTime, string deliveryAddress, string recipientFirstName, string recipientLastName, int postcode, string city, float totalPrice, int idOrderStatus, string orderStatus, string restaurantName, string restaurantAddress, int restaurantPostCode, string restaurantCity)
        {
            IdOrder = idOrder;
            OrderDate = orderDate;
            DeliveryTime = deliveryTime;
            DeliveryAddress = deliveryAddress;
            RecipientFirstName = recipientFirstName;
            RecipientLastName = recipientLastName;
            Postcode = postcode;
            City = city;
            TotalPrice = totalPrice;
            IdOrderStatus = idOrderStatus;
            OrderStatus = orderStatus;
            RestaurantName = restaurantName;
            RestaurantAddress = restaurantAddress;
            RestaurantPostCode = restaurantPostCode;
            RestaurantCity = restaurantCity;
        }

        public OrdersList(int idOrder, DateTime orderDate, DateTime deliveryTime, string deliveryAddress, string recipientFirstName, string recipientLastName, int postcode, string city, float totalPrice, int idOrderStatus, string orderStatus, string restaurantName, string restaurantAddress)
        {
            IdOrder = idOrder;
            OrderDate = orderDate;
            DeliveryTime = deliveryTime;
            DeliveryAddress = deliveryAddress;
            RecipientFirstName = recipientFirstName;
            RecipientLastName = recipientLastName;
            Postcode = postcode;
            City = city;
            TotalPrice = totalPrice;
            IdOrderStatus = idOrderStatus;
            OrderStatus = orderStatus;
            RestaurantName = restaurantName;
            RestaurantAddress = restaurantAddress;
        }
    }
}
