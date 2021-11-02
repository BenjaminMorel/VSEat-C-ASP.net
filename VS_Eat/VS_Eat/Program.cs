using BLL;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Channels;

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


            /** Get all restaurants 
            Console.WriteLine("______________________________________________");
            Console.WriteLine("All restaurants:");
            List<Restaurant> allRestaurants = RestaurantManager.GetAllRestaurants();
            foreach (var Restaurant in allRestaurants)
                Console.WriteLine(Restaurant.ToString());
            **/


            /** Get all orders
            Console.WriteLine("______________________________________________");
            Console.WriteLine("All orders:");
            List<Order> allOrders = OrderManager.GetAllOrders();
            foreach (var Order in allOrders)
                Console.WriteLine(Order.ToString());
            **/

            /** Get all orders from a restaurant
            Console.WriteLine("______________________________________________");
            var RestaurantId = 1;
            Console.WriteLine("All products for restaurant: " + RestaurantId);
            List<Product> productsFromRestaurant = ProductManager.GetAllProductsFromRestaurant(RestaurantId);
            foreach (var Product in productsFromRestaurant)
                Console.WriteLine(Product.ToString());
            **/

            /** Get all users 
            Console.WriteLine("______________________________________________");
            Console.WriteLine("All users: ");
            List<User> allUsers = UserManager.GetAllUsers();
            foreach (var User in allUsers)
                Console.WriteLine(User.ToString());
            **/

        }

    }
}
