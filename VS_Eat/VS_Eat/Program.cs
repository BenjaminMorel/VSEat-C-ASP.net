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

        //TODO corriger getter setter DTO money /date 
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

            /*
            (DEFAULT, '18:30:00', 'Rue de la faim 3', '4.00', '30.00', '1', '3', '1', '40'),
            (DEFAULT, '17:45:00', 'Rue de la faim 12', '8.00', '60.00', '1', '4', '2', '88');
            */

            Order MyOrder = new Order();
            MyOrder.IdOrder = 4; 
            MyOrder.DeliveryTime = "19:15:00";
            MyOrder.DeliveryAddress = "Rue du flip reset";
            MyOrder.Freight = 10;
            MyOrder.TotalPrice = 150; 
            MyOrder.IdOrderStatus = 4;
            MyOrder.IdUser = 2;
            MyOrder.IdLocation = 3;


            User MyUser = new User();
            MyUser.LastName = "Vouillamoz";
            MyUser.FirstName = "Hugo";
            MyUser.IdUser = 2; 
            OrderManager.CancelOrder(MyOrder,MyUser); 
            /**var MyProduct = new Product();

            MyProduct.IdProduct = 1;
            MyProduct.ProductName = "Cheuger";
            MyProduct.Description = "Burger LES VEGGGGGIS";
            MyProduct.Price = 15;
            MyProduct.Picture = "ceBurge.png";
            MyProduct.Disponibility = false;
            MyProduct.Vegetarian = true;
            MyProduct.IdRestaurant = 1;
            MyProduct.IdProductType = 3;

            ProductManager.UpdateProduct(MyProduct);**/


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

            /** Get all products 
            Console.WriteLine("______________________________________________");
            Console.WriteLine("All products:");
            List<Product> allProducts = ProductManager.GetAllProducts();
            foreach (var product in allProducts)
                Console.WriteLine(product.ToString());
            **/

        }

    }
}
