﻿using BLL;
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

            var UserManager = new UserManager(Configuration); 
            UserManager.addNewUser("Hugo","Vouillamoz","05805","Test@test4","password","rue du chateau", 1945,"Liddes");

        }
    
    }
}
