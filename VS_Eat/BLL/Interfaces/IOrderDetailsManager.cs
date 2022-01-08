using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderDetailsManager
    {
        List<OrderDetails> GetOrderDetailsFromOrder(int IdOrder);

        List<OrderDetails> GetAllOrderDetails();

        OrderDetails AddOrderDetails(OrderDetails MyOrderDetails); 
    }
}
