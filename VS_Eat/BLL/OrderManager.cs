﻿using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class OrderManager
    {
        private IOrderDB OrderDb { get; }

        public OrderManager(IConfiguration configuration)
        {
            OrderDb = new OrderDB(configuration); 
        }

        public void ShowOrder()
        {
            OrderDb.ShowOrder(); 
        }

        public List<Order> GetOrderByUser(int IdUser)
        {
            return OrderDb.GetOrderByUser(IdUser); 
        }
    }
}
