using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IOrderManager
    {
        List<Order> GetAllOrders();

        List<Order> GetOrderByUser(int IdUser);

        List<Order> GetOpenOrdersFromRegion(int IdRegion);

        List<Order> GetAllOrderFromRestaurant(int IdRestaurant);

        List<Order> GetAllOrderFromDeliveryStaff(int IdDeliveryStaff);

        Order GetOrderById(int IdOrder);

        Order AddNewOrder(Order MyOrder);

        Order CancelOrder(Order MyOrder, User MyUser);

        Order UpdateOrderStatus(Order MyOrder);

        Order AssignStaffToAnOrder(Order MyOrder);
    }
}
