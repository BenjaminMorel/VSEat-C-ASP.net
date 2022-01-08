using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Order
    {
        public int IdOrder { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime DeliveryTime { get; set; }

        public string DeliveryAddress { get; set; }

        public float Freight { get; set; }

        public float TotalPrice { get; set; }

        public int IdOrderStatus { get; set; }

        public int IdUser { get; set; }

        public int IdDeliveryStaff { get; set; }

        public int IdLocation { get; set; }

        public int IdRestaurant { get; set;  }

        public Order()
        {

        }
        public Order(DateTime orderDate, DateTime deliveryTime, string deliveryAddress, float freight, float totalPrice, int idOrderStatus, int idUser, int idLocation, int idRestaurant)
        {
            OrderDate = orderDate;
            DeliveryTime = deliveryTime;
            DeliveryAddress = deliveryAddress;
            Freight = freight;
            TotalPrice = totalPrice;
            IdOrderStatus = idOrderStatus;
            IdUser = idUser;
            IdLocation = idLocation;
            IdRestaurant = idRestaurant;
        }
    }
}
