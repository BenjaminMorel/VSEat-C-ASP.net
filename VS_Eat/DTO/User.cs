using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
	//TODO changer les getter/setteur pour ajouter les 2 string de favoris
	public class User
	{
		public int IdUser { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set;  }

		public string PhoneNumber { get; set; }

		public string Address { get; set; }

		public int IdLogin { get; set; }

		public int IdLocation { get; set; }

		public string FavoriteRestaurant { get; set; }

		public string FavoriteProduct { get; set; }

		public override string ToString()
		{
			return "__________________________" +
				   "\nId User: " + IdUser +
				   "\nFirstname: " + FirstName +
				   "\nLastname: " + LastName +
				   "\nPhoneNumber: " + PhoneNumber +
				   "\nEmailAddress: " + Address +
				   "\nId Login: " + IdLogin +
				   "\nId Location: " + IdLocation +
				   "\n__________________________";
		}

	}
}
