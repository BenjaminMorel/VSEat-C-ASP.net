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
        private ILocationManager locationManager { get; }
        private IOrderStatusManager orderStatusManager { get; }
        private IUserManager userManager { get; }

        private IRestaurantManager restaurantManager { get; }

        public DeliveryStaffController(IDeliveryStaffManager deliveryStaffManager, IOrderManager orderManager, ILocationManager locationManager, IOrderStatusManager orderStatusManager,
            IUserManager userManager, IRestaurantManager restaurantManager)
        {
            this.deliveryStaffManager = deliveryStaffManager;
            this.orderManager = orderManager;
            this.locationManager = locationManager;
            this.orderStatusManager = orderStatusManager;
            this.userManager = userManager;
            this.restaurantManager = restaurantManager;
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
                double myTotalPrice = order.TotalPrice;
                var myStatus = orderStatusManager.GetOrderStatus(order.IdOrder);
                var myRestaurant = restaurantManager.GetRestaurantByID(order.IdRestaurant);

                myOrderList.OrderNumber = order.IdOrder;
                myOrderList.OrderDate = order.OrderDate;
                myOrderList.DeliveryTime = order.DeliveryTime;
                myOrderList.DeliveryAddress = order.DeliveryAddress;
                myOrderList.RecipientFirstName = myUser.FirstName;
                myOrderList.RecipientLastName = myUser.LastName;
                myOrderList.Postcode = myLocation.PostCode;
                myOrderList.City = myLocation.City;
                myOrderList.TotalPrice = myTotalPrice;
                myOrderList.IdOrderStatus = order.IdOrderStatus;
                myOrderList.OrderStatus = myStatus.Status;
                myOrderList.RestaurantName = myRestaurant.RestaurantName;
                myOrderList.RestaurantAddress = myRestaurant.RestaurantAddress;

                ordersList.Add(myOrderList);
            }

            return View(ordersList);

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int IdOrder)
        {

            var myOrder = orderManager.GetOrderById(IdOrder);
            myOrder.IdOrderStatus = 4;
            orderManager.UpdateOrderStatus(myOrder);

            var IdStaff = (int) HttpContext.Session.GetInt32("ID_LOGIN");
            var myDeliveryStaff = deliveryStaffManager.GetDeliveryStaffByID(IdStaff);
            var allOrders = deliveryStaffManager.CountOpenOrderByStaffID(myDeliveryStaff.IdDeliveryStaff);
            List<OrdersList> ordersList = new List<OrdersList>();

            foreach (var order in allOrders)
            {
                OrdersList myOrderList = new OrdersList();
                var myLocation = locationManager.GetLocationByID(order.IdLocation);
                var myUser = userManager.GetUserByID(order.IdUser);
                double myTotalPrice = order.TotalPrice;
                var myStatus = orderStatusManager.GetOrderStatus(order.IdOrder);
                var myRestaurant = restaurantManager.GetRestaurantByID(order.IdRestaurant);

                myOrderList.OrderNumber = order.IdOrder;
                myOrderList.OrderDate = order.OrderDate;
                myOrderList.DeliveryTime = order.DeliveryTime;
                myOrderList.DeliveryAddress = order.DeliveryAddress;
                myOrderList.RecipientFirstName = myUser.FirstName;
                myOrderList.RecipientLastName = myUser.LastName;
                myOrderList.Postcode = myLocation.PostCode;
                myOrderList.City = myLocation.City;
                myOrderList.TotalPrice = myTotalPrice;
                myOrderList.IdOrderStatus = order.IdOrderStatus;
                myOrderList.OrderStatus = myStatus.Status;
                myOrderList.RestaurantName = myRestaurant.RestaurantName;
                myOrderList.RestaurantAddress = myRestaurant.RestaurantAddress;

                ordersList.Add(myOrderList);
            }

            return View(ordersList);
        }
        public IActionResult DetailsOrder(int IdOrder)
        {
            OrdersList myOrderList = new OrdersList();
            var myOrder = orderManager.GetOrderById(IdOrder);

            var myUser = userManager.GetUserByID(myOrder.IdUser);
            double myTotalPrice = myOrder.TotalPrice;
            var myStatus = orderStatusManager.GetOrderStatus(myOrder.IdOrder);
            var myRestaurant = restaurantManager.GetRestaurantByID(myOrder.IdRestaurant);
            var myLocation = locationManager.GetLocationByID(myOrder.IdLocation);
            var restaurantLocation = locationManager.GetLocationByID(myRestaurant.IdLocation);

            myOrderList.OrderNumber = myOrder.IdOrder;
            myOrderList.OrderDate = myOrder.OrderDate;
            myOrderList.DeliveryTime = myOrder.DeliveryTime;
            myOrderList.DeliveryAddress = myOrder.DeliveryAddress;
            myOrderList.RecipientFirstName = myUser.FirstName;
            myOrderList.RecipientLastName = myUser.LastName;
            myOrderList.Postcode = myLocation.PostCode;
            myOrderList.City = myLocation.City;
            myOrderList.TotalPrice = myTotalPrice;
            myOrderList.IdOrderStatus = myOrder.IdOrderStatus;
            myOrderList.OrderStatus = myStatus.Status;
            myOrderList.RestaurantName = myRestaurant.RestaurantName;
            myOrderList.RestaurantAddress = myRestaurant.RestaurantAddress;
            myOrderList.RestaurantPostCode = restaurantLocation.PostCode;
            myOrderList.RestaurantCity = restaurantLocation.City;

            return View(myOrderList);
        }
    }
}
