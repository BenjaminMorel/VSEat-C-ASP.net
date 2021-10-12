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
            var user1 = new UserDB(Configuration);
            var restaurant1 = new RestaurantDB(Configuration);
            var login1 = new LoginDB(Configuration);
            var order1 = new OrderDB(Configuration);

            Console.Write("RESTAURANT\n");
            user1.ShowUser();

            Console.Write("USER\n");
            restaurant1.ShowRestaurant();

            Console.Write("LOGIN\n");
            login1.ShowLogin();

            Console.Write("Order\n");
            order1.ShowOrder();

            Console.Write("END OF THE PROGRAM");
        }
    
    }
}
