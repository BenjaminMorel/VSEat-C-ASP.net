
using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IRestaurantDB
    {
        List<Restaurant> GetAllRestaurants(); 

        //TODO Add new restaurant (return restaurant)

        //TODO Update restaurant

    }
}
