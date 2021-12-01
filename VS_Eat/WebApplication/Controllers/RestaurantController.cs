using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace WebApplication.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantManager RestaurantManager { get; }


        public RestaurantController(IRestaurantManager RestaurantManager)
        {
            this.RestaurantManager = RestaurantManager; 
        }
        public IActionResult Index()
        {
            var restaurants = RestaurantManager.GetAllRestaurants(); 
            return View(restaurants);
        }
    }
}
