using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace BLL
{
    public class OrderManager : IOrderManager
    {
        private IOrderDB OrderDb { get; }

        public OrderManager(IConfiguration configuration)
        {
            OrderDb = new OrderDB(configuration); 
        }

        public List<Order> GetAllOrders()
        {
            return OrderDb.GetAllOrders(); 
        }

        public List<Order> GetOrderByUser(int IdUser)
        {
            return OrderDb.GetOrderByUser(IdUser); 
        }
    }
}
