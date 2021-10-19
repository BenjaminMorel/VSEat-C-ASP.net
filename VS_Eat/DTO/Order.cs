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
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public float Freight { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Free { get; set; }
        public bool Finished { get; set; }
        public bool Archived { get; set; }
        public int IdUser { get; set; }
        public int IdDeliveryStaff { get; set; }

        public override string ToString()
        {
            return "\n__________________________\n" +
                "\nID Order :" + IdOrder +
                "\nOrderNumber : " + OrderNumber +
                "\nOrder Date : " + OrderDate +
                "\nFreight : " + Freight +
                "\nDelivery Address : " + DeliveryAddress +
                "\nFree ? " + Free +
                "\nFinished ? " + Finished +
                "\nArchived ? " + Archived +
                "\n Id User : " + IdUser +
                "\nId Delivery Staff : " + IdDeliveryStaff; 

        }
    }
}
