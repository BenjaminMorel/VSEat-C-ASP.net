using DAL;
using Microsoft.Extensions.Configuration;
using System;
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
            var UserDB = new UserDB(Configuration);
            var LoginDB = new LoginDB(Configuration);
            User myUser = null;
           
           myUser = UserDB.GetUserByID("hugo.vouillamoz@students.hevs.ch", "4567");

            if(myUser != null)
            {
                Console.WriteLine(myUser.ToString()); ;
            }
            else
            {
                Console.WriteLine("Email or Password incorrect");
            }


            var ProductDB = new ProductDB(Configuration);
            var allProducts = ProductDB.GetAllProductsFromRestaurant(2);
            foreach (var product in allProducts)
            {
                Console.WriteLine(product.ToString());
            }

           
        }
    
    }
}
