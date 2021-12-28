using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private IOrderManager OrderManager { get; }
        private IOrderDetailsManager OrderDetailsManager { get; }

        public OrderController(IOrderManager OrderManager)
        {
            this.OrderManager = OrderManager;
            this.OrderDetailsManager = OrderDetailsManager; 
        }
        public IActionResult ShowAllOrders()
        {
            var orders = OrderManager.GetOrderByUser((int)HttpContext.Session.GetInt32("ID_USER"));
            return View(orders);
        }

        public IActionResult ShowOrderDetail(int IdOrder)
        {
            var orderDetails = OrderDetailsManager.GetOrderDetailsFromOrder(IdOrder); 
            return View(orderDetails); 
        }
    }
}
