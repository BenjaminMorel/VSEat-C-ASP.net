using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DeliveryStaffController : Controller
    {

        private IDeliveryStaffManager deliveryStaffManager { get; }
        private IOrderManager orderManager { get; }
        private IRegionManager regionManager { get; }
        private ILocationManager locationManager { get; }
        private IOrderStatusManager orderStatusManager { get; }
        private IUserManager userManager { get; }

        public DeliveryStaffController(IDeliveryStaffManager deliveryStaffManager, IOrderManager orderManager, IRegionManager regionManager, ILocationManager locationManager, IOrderStatusManager orderStatusManager, IUserManager userManager)
        {
            this.deliveryStaffManager = deliveryStaffManager;
            this.orderManager = orderManager;
            this.regionManager = regionManager;
            this.locationManager = locationManager;
            this.orderStatusManager = orderStatusManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var IdStaff = (int) HttpContext.Session.GetInt32("ID_LOGIN");
            var myDeliveryStaff = deliveryStaffManager.GetDeliveryStaffByID(IdStaff);
            var allOrders = deliveryStaffManager.CountOpenOrderByStaffID(myDeliveryStaff.IdDeliveryStaff);
            List<OrdersList> ordersList = new List<OrdersList>();

            foreach (var order in allOrders)
            {
                OrdersList myOrderList = new OrdersList();
                var myLocation = locationManager.GetLocationByID(order.IdLocation);
                var myUser = userManager.GetUserByID(order.IdUser);
                var myTotalPrice = order.TotalPrice + order.Freight;
                var myStatus = orderStatusManager.GetOrderStatus(order.IdOrder);

                myOrderList.OrderNumber = order.IdOrder;
                myOrderList.OrderDate = order.OrderDate;
                myOrderList.DeliveryTime = order.DeliveryTime;
                myOrderList.DeliveryAddress = order.DeliveryAddress;
                myOrderList.FirstName = myUser.FirstName;
                myOrderList.LastName = myUser.LastName;
                myOrderList.Postcode = myLocation.PostCode;
                myOrderList.City = myLocation.City;
                myOrderList.TotalPrice = myTotalPrice;
                myOrderList.OrderStatus = myStatus.Status;

                ordersList.Add(myOrderList);
            }

            return View(ordersList);

        }

    }
}
