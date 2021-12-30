using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL.Interfaces
{
    public interface IOrderManager
    {
        List<Order> GetAllOrders();

        List<Order> GetOrderByUser(int IdUser);

        List<Order> GetOpenOrdersFromRegion(int IdRegion);

        List<Order> GetAllOrderFromRestaurant(int IdRestaurant);

        Order GetOrderById(int IdOrder);

        Order AddNewOrder(Order MyOrder);

        Order CancelOrder(Order MyOrder, User MyUser);

        Order UpdateOrderStatus(Order MyOrder);
    }
}
