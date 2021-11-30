using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DTO;

namespace BLL
{
    public class RestaurantManager : IRestaurantManager
    {
        private IRestaurantDB RestaurantDb { get;  }

        public RestaurantManager(IRestaurantDB iRestaurantDb)
        {
            this.RestaurantDb = iRestaurantDb;
        }

        public List<Restaurant> GetAllRestaurants()
        {
           return RestaurantDb.GetAllRestaurants(); 
        }

        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            return RestaurantDb.AddRestaurant(restaurant);
        }

        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            return RestaurantDb.UpdateRestaurant(restaurant);
        }
    }
}
