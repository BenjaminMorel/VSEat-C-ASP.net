

using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrderDB
    {
        List<Order> GetAllOrders();

        List<Order> GetOrderByUser(int IdUser);

        List<Order> GetOpenOrdersFromRegion(int IdRegion);

        //TODO: add a method to be able to add an order in the database with all foreign key 

    }
}
