

using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrderDB
    {
        void ShowAllOrders();

        List<Order> GetOrderByUser(int IdUser); 
    }
}
