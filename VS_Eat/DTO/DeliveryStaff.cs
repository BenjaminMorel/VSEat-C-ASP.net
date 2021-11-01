using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DeliveryStaff
    {
        public int IdDeliveryStaff { get; set; }

        public string FirstNameDeliveryStaff { get; set; }

        public string NameDeliveryStaff { get; set; }

        public int IdLogin { get; set; }

        public int IdLocation { get; set; }

        public override string ToString()
        {
            return "\n__________________________" +
                    "\nIdDeliveryStaff: " + IdDeliveryStaff +
                    "\nFirstName: " + FirstNameDeliveryStaff +
                    "\nLastName: " + NameDeliveryStaff +
                    "\nIdLogin: " + IdLogin +
                    "\nIdLocation:"  + IdLocation +
                    "\n__________________________";
        }
    }
}
