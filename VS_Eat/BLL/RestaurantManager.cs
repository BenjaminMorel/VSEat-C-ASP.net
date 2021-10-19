using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RestaurantManager
    {
        private IRestaurantDB RestaurantDb { get;  }

        public RestaurantManager(IConfiguration configuration)
        {
            RestaurantDb = new RestaurantDB(configuration); 
        }

        public void ShowRestaurant()
        {
           RestaurantDb.ShowRestaurant(); 
        }
    }
}
