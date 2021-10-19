using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class DeliveryStaff
    {
        public int IdDeliveryStaff { get; set; }
        public string FirstNameDeliveryStaff { get; set; }
        public string NameDeliveryStaff { get; set; }
        public string IdLogin { get; set; }

        public override string ToString()
        {
            return "\n__________________________\n" +
                "\nIdDevlieryStaff : " + IdDeliveryStaff +
                "\nFirstNameDeliveryStaff : " + FirstNameDeliveryStaff +
                "\nNameDeliveryStaff : " + NameDeliveryStaff +
                "\nIdLogin : " + IdLogin;
        }
    }
}
