using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Restaurant
    {
        public int IdRestaurant { get; set; }

        public string RestaurantName { get; set; }

        public string RestaurantAddress { get; set; }

        public int IdLogin { get; set; } 

        public int IdLocation { get; set; }

        public override string ToString() {

            return "__________________________"+ 
                   "\nRestaurant: " + IdRestaurant + 
                   "\nNom: " + RestaurantName + 
                   "\nAddress: " + RestaurantAddress +
                   "\nIdLogin: " + IdLogin + 
                   "\nIdLocation: " + IdLocation +
                   "\n__________________________"; 
        }
    }
}
