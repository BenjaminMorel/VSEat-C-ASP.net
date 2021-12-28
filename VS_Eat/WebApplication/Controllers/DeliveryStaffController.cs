using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Controllers
{
    public class DeliveryStaffController : Controller
    {

        private IDeliveryStaffManager deliveryStaffManager { get; }
        private IOrderManager orderManager { get; }

        public DeliveryStaffController(IDeliveryStaffManager deliveryStaffManager, IOrderManager orderManager)
        {
            this.deliveryStaffManager = deliveryStaffManager;
            this.orderManager = orderManager;
        }

        public IActionResult Index()
        {
            //var id = (int) HttpContext.Session.GetInt32("ID_LOGIN");
            //var Orders = deliveryStaffManager.CountOpenOrderByStaffID(id);
            var Orders = orderManager.GetAllOrders();
            return View(Orders);
        }
    }
}
