
using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IRestaurantDB
    {
        List<Restaurant> GetAllRestaurants();

        Restaurant GetRestaurantByID(int IdRestaurant);

        Restaurant AddRestaurant(Restaurant restaurant);

        Restaurant UpdateRestaurant(Restaurant restaurant);

    }
}
