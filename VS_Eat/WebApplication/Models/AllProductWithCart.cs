using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class AllProductWithCart
    {
        public List<Product> products { get; set; }

        public List<CartDetails> myCart { get; set; }

        public int IdRestaurant { get; set; }

        public string RestaurantName { get; set;  }

        public List<string> Comment { get; set; }

        public double TotalPrice { get; set;  }

        public AllProductWithCart()
        {

        }
        public AllProductWithCart(List<Product> products, List<CartDetails> myCart, int idRestaurant, List<string> comment,string RestaurantName)
        {
            this.products = products;
            this.myCart = myCart;
            IdRestaurant = idRestaurant;
            Comment = comment;
            this.RestaurantName = RestaurantName; 
            foreach(var product in myCart)
            {
                TotalPrice += product.UnitPrice * product.Quantity; 
            }
        }
    }
}
