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
        
        public List<Region> RegionName { get; set;}


    }
}