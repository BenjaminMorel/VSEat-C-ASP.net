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

        public string OrderDate { get; set; }

        public string DeliveryAddress { get; set; }

        public float Freight { get; set; }

        public float TotalPrice { get; set; }

        public int IdOrderStatus { get; set; }

        public int IdUser { get; set; }

        public int IdDeliveryStaff { get; set; }

        public int IdLocation { get; set; }

        public override string ToString()
        {
            return "__________________________" +
                   "\nId Order: " + IdOrder +
                   "\nOrder Date: " + OrderDate +
                   "\nDelivery Address: " + DeliveryAddress +
                   "\nFreight: " + Freight +
                   "\nTotalPrice: " + TotalPrice +
                   "\nIdOrderStatus: " + IdOrderStatus +
                   "\nIdUser: " + IdUser +
                   "\nIdDelivery Staff: " + IdDeliveryStaff +
                   "\nIdLocation: " + IdLocation +
                   "\n__________________________";
        }
    }
}
