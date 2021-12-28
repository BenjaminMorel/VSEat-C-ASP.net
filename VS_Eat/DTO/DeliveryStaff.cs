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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public int IdLogin { get; set; }

        public int IdLocation { get; set; }

        public int IdDeliveryStaffType { get; set; }

        public int IdWorkingRegion { get; set; }

        public override string ToString()
        {
            return "\n__________________________" +
                    "\nIdDeliveryStaff: " + IdDeliveryStaff +
                    "\nFirstName: " + FirstName +
                    "\nLastName: " + LastName +
                    "\nIdLogin: " + IdLogin +
                    "\nIdLocation:"  + IdLocation +
                    "\nIdDeliveryStaffType:" + IdDeliveryStaffType +
                    "\nIdWorkingRegion:" + IdWorkingRegion +
                    "\n__________________________";
        }
    }
}
