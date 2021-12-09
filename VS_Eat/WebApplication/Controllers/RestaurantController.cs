using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Controllers
{
    public class RestaurantController : Controller
    {
        private IRestaurantManager RestaurantManager { get; }
        private IProductManager ProductManager { get; }
        private ILocationManager LocationManager { get; }


        public RestaurantController(IRestaurantManager RestaurantManager, IProductManager ProductManager, ILocationManager LocationManager )
        {
            this.RestaurantManager = RestaurantManager;
            this.ProductManager = ProductManager;
            this.LocationManager = LocationManager;
        }
        public ActionResult Index()
        {
            if(HttpContext.Session.GetInt32("ID_LOGIN") == null)
            {
                //linge pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account"); 
            }
            var restaurants = RestaurantManager.GetAllRestaurants();
            var locations = LocationManager.GetAllLocations();
           return View(restaurants);

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
    }
}
