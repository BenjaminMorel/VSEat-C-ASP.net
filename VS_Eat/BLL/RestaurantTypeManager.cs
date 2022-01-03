using BLL.Interfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class RestaurantTypeManager : IRestaurantTypeManager
    {
        private IRestaurantTypeDB restaurantTypeDB { get;  }

        public RestaurantTypeManager(IRestaurantTypeDB restaurantTypeDB)
        {
            this.restaurantTypeDB = restaurantTypeDB; 
        }
        public List<RestaurantType> GetAllRestaurantType()
        {
            return restaurantTypeDB.GetAllRestaurantType(); 
        }
    }
}
