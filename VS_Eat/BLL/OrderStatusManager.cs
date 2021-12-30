using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class OrderStatusManager :IOrderStatusManager
    {
        private IOrderStatusDB OrderStatusDB { get; }

        public OrderStatusManager(IOrderStatusDB orderStatusDb)
        {
            this.OrderStatusDB = orderStatusDb;
        }

        public List<OrderStatus> GetAllOrderStatus()
        {
            return OrderStatusDB.GetAllOrderStatus();
        }

        public OrderStatus GetOrderStatus(int IdOrder)
        {
            return OrderStatusDB.GetOrderStatus(IdOrder);
        }
    }
}
