using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using DTO;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantManager RestaurantManager { get; }
        private IProductManager ProductManager { get; }
        private ILocationManager LocationManager { get; }

        private IUserManager UserManager { get; }

        private IRegionManager RegionManager { get; }


        public RestaurantController(IRestaurantManager RestaurantManager, IProductManager ProductManager, ILocationManager LocationManager, IUserManager UserManager, IRegionManager RegionManager)
        {
            this.RestaurantManager = RestaurantManager;
            this.ProductManager = ProductManager;
            this.LocationManager = LocationManager;
            this.UserManager = UserManager;
            this.RegionManager = RegionManager; 
        }
        public ActionResult Index()
        {
            if(HttpContext.Session.GetInt32("ID_LOGIN") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account"); 
            }
            var restaurants = RestaurantManager.GetAllRestaurants();
            var regions = RegionManager.GetAllRegions();
            RestaurantToDisplay allData = new RestaurantToDisplay();

            allData.allRestaurant = restaurants;
            allData.RegionName = regions;
            return View(allData);

        }


        public ActionResult ShowAllProductFromRestaurant(int id)
        {
            var products = ProductManager.GetAllProductsFromRestaurant(id); 
            return View(products); 
        }

        public ActionResult ProductDetails(int id)
        {
            var product = ProductManager.GetProductByID(id);
            Console.WriteLine(product.ToString());
            return View(product); 
        }

        public ActionResult AllRestaurantsByRegion()
        {
            var myUser = UserManager.GetUserByID((int)HttpContext.Session.GetInt32("ID_LOGIN"));
            var User_Location = LocationManager.GetLocationByID(myUser.IdLocation); 
            var restaurants = RestaurantManager.GetAllRestaurants();
            var RestaurantsFromMyRegion = new List<Restaurant>();
            List<Region> region = new List<Region>(); 
            region.Add(RegionManager.GetRegionName(User_Location.IdRegion)); 
            
            var allData = new RestaurantToDisplay();

            foreach (var restaurant in restaurants)
            {
                var location = LocationManager.GetLocationByID(restaurant.IdLocation); 
                if(location.IdRegion == User_Location.IdRegion)
                {
                    RestaurantsFromMyRegion.Add(restaurant); 
                }
            }

    
            allData.allRestaurant = RestaurantsFromMyRegion;
            allData.RegionName = region; 

            return View(allData); 
        }


    }
}
