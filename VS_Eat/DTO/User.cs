using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class User
    {

        public int IdUser { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set;  }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public int IdLogin { get; set; }
        public int IdLocation { get; set; }

        public override string ToString()
        {
            return "\n__________________________\n\n" +
                "ID User : " + IdUser +
                "\nFirstname : " + FirstName +
                "\nLastname : " + LastName +
                "\nPhoneNumber : " + PhoneNumber +
                "\nEmailAddress : " + Address +
                "\nID Login : " + IdLogin +
                "\nID Location : " + IdLocation + "\n__________________________\n"; 
        }

    }
}
