

using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IOrderDB
    {
        void ShowOrder();

        List<Order> GetOrderByUser(int IdUser); 
    }
}
