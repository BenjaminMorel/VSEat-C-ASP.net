

using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrderDB
    {
        List<Order> GetAllOrders();

        List<Order> GetOrderByUser(int IdUser);

        List<Order> GetOpenOrdersFromRegion(int IdRegion);

        Order AddNewOrder(Order MyOrder);

        //UpdateStatus is used to change the Order Status or to cancelled it 
        Order CancelOrder(Order MyOrder, User MyUser);

        Order UpdateOrderStatus(Order MyOrder); 

    }
}
