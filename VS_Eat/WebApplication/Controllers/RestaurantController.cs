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
        private IProductManager ProductManager { get; }



        public RestaurantController(IRestaurantManager RestaurantManager, IProductManager ProductManager)
        {
            this.RestaurantManager = RestaurantManager;
            this.ProductManager = ProductManager; 
        }
        public ActionResult Index()
        {
            var restaurants = RestaurantManager.GetAllRestaurants(); 
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
