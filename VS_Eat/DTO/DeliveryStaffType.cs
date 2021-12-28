using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DeliveryStaffType
    {
        public int IdDeliveryStaffType { get; set; }

        public string DeliveryStaffTypeStr { get; set; }

        public override string ToString()
        {
            return "__________________________" +
                   "\nIdDeliveryStaffType: " + IdDeliveryStaffType +
                   "\nDeliveryStaffTypeStr: " + DeliveryStaffTypeStr +
                   "\n__________________________";
        }

    }
}
