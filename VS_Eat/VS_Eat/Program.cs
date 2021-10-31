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

            var LoginManager = new LoginManager(Configuration);
            var OrderManager = new OrderManager(Configuration);
            var UserManager = new UserManager(Configuration);
       //     UserManager.CreateNewUser("Hugo", "Vouillamoz", "959595", "test@test3", "password", "Rue chateau", 1945,
           //     "Liddes"); 

            //   UserManager.ShowAllUser();
            //   LoginManager.ShowAllLogin();
            //   var DeliveryManager = new DeliveryStaffManager(Configuration);
            //    Console.WriteLine(DeliveryManager.CountOpenOrderByStaffID(1));

        }

    }
}
