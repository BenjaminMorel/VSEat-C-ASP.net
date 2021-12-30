
using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IRestaurantDB
    {
        List<Restaurant> GetAllRestaurants();

        Restaurant GetRestaurantByID(int IdRestaurant);

        Restaurant GetRestaurantByIDLogin(int IdLogin);
        Restaurant AddRestaurant(Restaurant restaurant);

        Restaurant UpdateRestaurant(Restaurant restaurant);

    }
}
