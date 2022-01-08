using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Review
    {
        public int IdRestaurant { get; set; }

        public int Stars { get; set; }

        public string Comment{get;set;}

        public Review()
        {

        }
        public Review(int idRestaurant, int stars)
        {
            IdRestaurant = idRestaurant;
            Stars = stars;
        }
    }
}
