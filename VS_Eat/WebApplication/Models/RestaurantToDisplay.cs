﻿using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class RestaurantToDisplay

    {
        public List<Restaurant> allRestaurant {get; set;} 

        public Region myRegion { get; set;  }
        
        public List<Region> RegionName { get; set;}

        public List<RestaurantType> AllRestaurantType { get; set;  }

        public List<string> AllTypeToDisplay { get; set;  }
        public List<ReviewToDisplay> AllReview { get; set; }

        public int total { get; set; }

    }
}
