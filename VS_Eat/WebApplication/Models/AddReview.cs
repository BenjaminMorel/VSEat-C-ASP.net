using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class AddReview
    {
        public int IdRestaurant { get; set; }

        public string RestaurantName { get; set; }

        public int Stars { get; set; }

        public string Comment { get; set; }

        public AddReview()
        {

        }
        public AddReview(int idRestaurant, string restaurantName)
        {
            IdRestaurant = idRestaurant;
            RestaurantName = restaurantName;
        }
    }
}
