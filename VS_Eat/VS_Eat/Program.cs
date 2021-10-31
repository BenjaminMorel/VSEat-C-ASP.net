using BLL;
using DAL;
using DTO;
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
            //var UserManager = new UserManager(Configuration);
            //UserManager.addNewUser("Hugo", "Vouillamoz", "05805", "Test@test12", 
            //  "password", "rue du chateau", 1945, "Liddes");
            //var DeliveryManager = new DeliveryStaffManager(Configuration);
            //Console.WriteLine(DeliveryManager.CountOpenOrderByStaffID(1));

            var OrderManager = new OrderManager(Configuration);
            OrderManager.ShowAllOrders();
            //Console.WriteLine(OrderManager.GetOrderByUser(1));


        }

    }
}
