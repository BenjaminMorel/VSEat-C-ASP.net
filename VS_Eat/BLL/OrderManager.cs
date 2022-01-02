using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace BLL
{
    public class OrderManager : IOrderManager
    {
        private IOrderDB OrderDb { get; }

        public OrderManager(IOrderDB orderDb)
        {
            this.OrderDb = orderDb;
        }

        public List<Order> GetAllOrders()
        {
            return OrderDb.GetAllOrders(); 
        }

        public List<Order> GetOrderByUser(int IdUser)
        {
            return OrderDb.GetOrderByUser(IdUser); 
        }

        public List<Order> GetOpenOrdersFromRegion(int IdRegion)
        {
            return OrderDb.GetOpenOrdersFromRegion(IdRegion); 
        }

        public List<Order> GetAllOrderFromRestaurant(int IdRestaurant)
        {
            return OrderDb.GetAllOrderFromRestaurant(IdRestaurant); 
        }

        public List<Order> GetAllOrderFromDeliveryStaff(int IdDeliveryStaff)
        {
            return OrderDb.GetAllOrderFromDeliveryStaff(IdDeliveryStaff); 
        }
        public Order GetOrderById(int IdOrder)
        {
            return OrderDb.GetOrderById(IdOrder); 
        }
        public Order AddNewOrder(Order MyOrder)
        {
            return OrderDb.AddNewOrder(MyOrder); 
        }

        public Order CancelOrder(Order MyOrder, User MyUser)
        {
            return OrderDb.CancelOrder(MyOrder, MyUser); 
        }

        public Order UpdateOrderStatus(Order MyOrder)
        {
            return OrderDb.UpdateOrderStatus(MyOrder); 
        }
        public Order AssignStaffToAnOrder(Order MyOrder)
        {

            //TODO trouver le staff qui livrera l'order 
            return OrderDb.AssignStaffToAnOrder(MyOrder); 
        }
    }
}
