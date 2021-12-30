using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRestaurantManager
    {
        List<Restaurant> GetAllRestaurants();

        Restaurant GetRestaurantByID(int IdRestaurant);

        Restaurant AddRestaurant(Restaurant restaurant);

        Restaurant UpdateRestaurant(Restaurant restaurant);
    }
}
