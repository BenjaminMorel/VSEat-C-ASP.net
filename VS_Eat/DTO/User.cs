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


        public User()
        {

        }

        public User(int idUser, string firstName, string lastName, string phoneNumber, string address, int idLogin, int idLocation)
        {
            IdUser = idUser;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Address = address;
            IdLogin = idLogin;
            IdLocation = idLocation;
        }
    }
}
