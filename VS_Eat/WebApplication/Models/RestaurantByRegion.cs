using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class RestaurantByRegion
    {
        public List<Restaurant> allRestaurant {get; set;} 
        
        public string RegionName { get; set;  }

    }
}
