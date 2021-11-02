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

        public RestaurantManager(IConfiguration configuration)
        {
            RestaurantDb = new RestaurantDB(configuration); 
        }

        public List<Restaurant> GetAllRestaurants()
        {
           return RestaurantDb.GetAllRestaurants(); 
        }
    }
}
