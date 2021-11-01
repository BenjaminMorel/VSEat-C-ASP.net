using BLL;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace VS_Eat
{
    class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

        static void Main(string[] args)
        {
            var DeliveryStaffManager = new DeliveryStaffManager(Configuration);
            var LoginManager = new LoginManager(Configuration);
            var OrderManager = new OrderManager(Configuration);
            var ProductManager = new ProductManager(Configuration);
            var RestaurantManager = new RestaurantManager(Configuration);
            var UserManager = new UserManager(Configuration);
            

            /** Get all restaurants **/
            Console.WriteLine("______________________________________________");
            Console.WriteLine("All restaurants:");
            List<Restaurant> allRestaurants = RestaurantManager.GetAllRestaurants();
            foreach (var Restaurant in allRestaurants)
                Console.WriteLine(Restaurant.ToString());

            /** Get all orders **/
            Console.WriteLine("______________________________________________");
            Console.WriteLine("All orders:");
            List<Order> allOrders = OrderManager.GetAllOrders();
            foreach (var Order in allOrders)
                Console.WriteLine(Order.ToString());


        }

    }
}
