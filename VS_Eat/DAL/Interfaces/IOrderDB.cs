

using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrderDB
    {
        List<Order> GetAllOrders();

        List<Order> GetOrderByUser(int IdUser); 
    }
}
