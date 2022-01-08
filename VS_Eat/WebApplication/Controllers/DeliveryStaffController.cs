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

        /// <summary>
        /// method to display the main page of staff member
        /// the page show to the delivery all his order (the archived one and the one that still must be delivered)
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("ID_STAFF") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }
            var IdStaff = (int) HttpContext.Session.GetInt32("ID_LOGIN");
            var myDeliveryStaff = deliveryStaffManager.GetDeliveryStaffByID(IdStaff);
            var allOrders = orderManager.GetAllOrderFromDeliveryStaff(myDeliveryStaff.IdDeliveryStaff);
            List<OrdersList> ordersList = new List<OrdersList>();

            foreach (var order in allOrders)
            {
                var myLocation = locationManager.GetLocationByID(order.IdLocation);
                var myUser = userManager.GetUserByIDUser(order.IdUser);      
                var myStatus = orderStatusManager.GetOrderStatus(order.IdOrder);
                var myRestaurant = restaurantManager.GetRestaurantByID(order.IdRestaurant);

                OrdersList myOrderList = new OrdersList(order.IdOrder, order.OrderDate, order.DeliveryTime, order.DeliveryAddress, myUser.FirstName,myUser.LastName,myLocation.PostCode,myLocation.City,
                    order.TotalPrice,order.IdOrderStatus,myStatus.Status,myRestaurant.RestaurantName,myRestaurant.RestaurantAddress);

                ordersList.Add(myOrderList);
            }

            return View(ordersList);

        }
        
        /// <summary>
        /// Http post method to change the status of an order from to be delivered to archived 
        /// </summary>
        /// <param name="IdOrder">the ID of the order that we want to change the status</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int IdOrder)
        {

            var myOrder = orderManager.GetOrderById(IdOrder);
            myOrder.IdOrderStatus = 4;
            orderManager.UpdateOrderStatus(myOrder);

            var IdStaff = (int)HttpContext.Session.GetInt32("ID_LOGIN");
            var myDeliveryStaff = deliveryStaffManager.GetDeliveryStaffByID(IdStaff);
            var allOrders = orderManager.GetAllOrderFromDeliveryStaff(myDeliveryStaff.IdDeliveryStaff);
            List<OrdersList> ordersList = new List<OrdersList>();

            foreach (var order in allOrders)
            {
 
                var myLocation = locationManager.GetLocationByID(order.IdLocation);
                var myUser = userManager.GetUserByIDUser(order.IdUser);
                var myStatus = orderStatusManager.GetOrderStatus(order.IdOrder);
                var myRestaurant = restaurantManager.GetRestaurantByID(order.IdRestaurant);
                OrdersList myOrderList = new OrdersList(order.IdOrder, order.OrderDate, order.DeliveryTime, order.DeliveryAddress, myUser.FirstName, myUser.LastName, myLocation.PostCode, myLocation.City,
                         order.TotalPrice, order.IdOrderStatus, myStatus.Status, myRestaurant.RestaurantName, myRestaurant.RestaurantAddress);

                ordersList.Add(myOrderList);
            }

            return View(ordersList);
        }
       
        /// <summary>
        /// Method to show the details of an order 
        /// </summary>
        /// <param name="IdOrder">the ID of the order that we want to see in detail</param>
        /// <returns></returns>
        public IActionResult DetailsOrder(int IdOrder)
        {
            if (HttpContext.Session.GetInt32("ID_STAFF") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }
            var myOrder = orderManager.GetOrderById(IdOrder);

            var myUser = userManager.GetUserByID(myOrder.IdUser);        
            var myStatus = orderStatusManager.GetOrderStatus(myOrder.IdOrder);
            var myRestaurant = restaurantManager.GetRestaurantByID(myOrder.IdRestaurant);
            var myLocation = locationManager.GetLocationByID(myOrder.IdLocation);
            var restaurantLocation = locationManager.GetLocationByID(myRestaurant.IdLocation);

            OrdersList myOrderList = new OrdersList(myOrder.IdOrder, myOrder.OrderDate, myOrder.DeliveryTime, myOrder.DeliveryAddress, myUser.FirstName, myUser.LastName, myLocation.PostCode, myLocation.City,
                                                    myOrder.TotalPrice, myOrder.IdOrderStatus, myStatus.Status, myRestaurant.RestaurantName, myRestaurant.RestaurantAddress,restaurantLocation.PostCode,restaurantLocation.City);


            return View(myOrderList);
        }
    }
}
