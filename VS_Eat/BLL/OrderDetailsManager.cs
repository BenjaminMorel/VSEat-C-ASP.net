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
    public class OrderDetailsManager : IOrderDetailsManager
    {
        private IOrderDetailsDB OrderDetailsDB { get;  }
        public OrderDetailsManager(IOrderDetailsDB OrderDetailsDB)
        {
            this.OrderDetailsDB = OrderDetailsDB; 
        }

        public List<OrderDetails> GetOrderDetailsFromOrder(int IdOrder)
        {
            return OrderDetailsDB.GetOrderDetailsFromOrder(IdOrder);
        }

        public List<OrderDetails> GetAllOrderDetails()
        {
            return OrderDetailsDB.GetAllOrderDetails();
        }

        public OrderDetails AddOrderDetails(OrderDetails MyOrderDetails)
        {
            return OrderDetailsDB.AddOrderDetails(MyOrderDetails); 
        }



    }
}
