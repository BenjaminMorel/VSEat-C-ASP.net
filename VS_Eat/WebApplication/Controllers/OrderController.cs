using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private IOrderManager OrderManager { get; }
        private IOrderStatusManager OrderStatusManager { get;  }
        private IOrderDetailsManager OrderDetailsManager { get; }
        private ICartDetailsManager CartDetailsManager { get;  }
        private ILocationManager LocationManager { get;  }
        private IRestaurantManager RestaurantManager { get; }

        public OrderController(IOrderManager OrderManager, IOrderDetailsManager orderDetailsManager, ICartDetailsManager CartDetailsManager, ILocationManager LocationManager, IRestaurantManager RestaurantManager, IOrderStatusManager OrderStatusManager)
        {
            this.OrderManager = OrderManager;
            this.OrderDetailsManager = orderDetailsManager;
            this.CartDetailsManager = CartDetailsManager;
            this.LocationManager = LocationManager;
            this.RestaurantManager = RestaurantManager;
            this.OrderStatusManager = OrderStatusManager; 
        }

        /// <summary>
        /// Method to display the page where a user can see all the order he had made 
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowAllOrders()
        {
            List<OrdersList> ordersList = new List<OrdersList>();
            var orders = OrderManager.GetOrderByUser((int)HttpContext.Session.GetInt32("ID_USER"));
            
            foreach(var order in orders)
            {
                var myLocation = LocationManager.GetLocationByID(order.IdLocation);
                var mystatus = OrderStatusManager.GetOrderStatus(order.IdOrderStatus);
                var myRestaurant = RestaurantManager.GetRestaurantByID(order.IdRestaurant);

                // aucun nom et prénom n'est spécifié vu qu'on montre toutes les commandes d'un seul client
                OrdersList myOrderList = new OrdersList(order.IdOrder, order.OrderDate, order.DeliveryTime, order.DeliveryAddress, null,null, myLocation.PostCode, myLocation.City,
                    order.TotalPrice, order.IdOrderStatus, mystatus.Status, myRestaurant.RestaurantName, myRestaurant.RestaurantAddress);
              
                ordersList.Add(myOrderList); 
            }
            return View(ordersList);
        }


        /// <summary>
        /// http post method where the user can cancelled an order (at least 3h before the delivery) 
        /// </summary>
        /// <param name="IdOrder">the id of the order we want to cancelled</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowAllOrders(int IdOrder)
        {
            // update de l'order status => l'order status deviens 5, la commande est donc annulée 
            var myOrder = OrderManager.GetOrderById(IdOrder);
            myOrder.IdOrderStatus = 5;
            OrderManager.UpdateOrderStatus(myOrder);

            List<OrdersList> ordersList = new List<OrdersList>();
            var orders = OrderManager.GetOrderByUser((int)HttpContext.Session.GetInt32("ID_USER"));

            foreach (var order in orders)
            {

                var myLocation = LocationManager.GetLocationByID(order.IdLocation);
                var mystatus = OrderStatusManager.GetOrderStatus(order.IdOrder);
                var myRestaurant = RestaurantManager.GetRestaurantByID(order.IdRestaurant);

                OrdersList myOrderList = new OrdersList(order.IdOrder, order.OrderDate, order.DeliveryTime, order.DeliveryAddress, null, null, myLocation.PostCode, myLocation.City,
                    order.TotalPrice, order.IdOrderStatus, mystatus.Status, myRestaurant.RestaurantName, myRestaurant.RestaurantAddress);
          

                ordersList.Add(myOrderList);
            }
            return View(ordersList);
        }


        /// <summary>
        /// method to display the details of an order 
        /// </summary>
        /// <param name="IdOrder">The id of the order we want to see in details</param>
        /// <returns></returns>
        public IActionResult ShowOrderDetail(int IdOrder)
        {
            var orderDetails = OrderDetailsManager.GetOrderDetailsFromOrder(IdOrder); 
            return View(orderDetails); 
        }

        /// <summary>
        /// Method to display the page where the user can confirm the content of his cart and validate the order
        /// </summary>
        /// <returns></returns>
        public IActionResult ConfirmOrder()
        {
            var myCartDetails = CartDetailsManager.GetAllCartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));

            ConfirmOrder myConfirmOrder = new ConfirmOrder(myCartDetails, 7);
            myConfirmOrder.errorCode = 0;
            return View(myConfirmOrder); 
        }


        /// <summary>
        /// Http post method that can do 3 different thing
        /// 1. Remove one element of the cart 
        /// 2. Remove all element of the cart 
        /// 3. verifiy that the region of the delivery is the same as the one of the restaurant
        /// 4. verify that the hour of the delivery is not passed
        /// 5. validate the order and insert it into the database
        /// </summary>
        /// <param name="DeliveryAddress">The adress where we want the delivery</param>
        /// <param name="city">The city of the delivery</param>
        /// <param name="PostCode">The post code of the delivery</param>
        /// <param name="IdCartDetails">The parameters that tell us if we want to confirm the order (IdCart== 0) or if we want to modify the cart</param>
        /// <param name="deliveryTime">The time of the delivery</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmOrder(string DeliveryAddress, string city, int PostCode, int IdCartDetails,DateTime deliveryTime)
        {
                var cartDetailsList = new List<CartDetails>();

                if (IdCartDetails != 0)
                {
                    if (IdCartDetails == -1)
                    {
                        CartDetailsManager.DeleteAllEntryByLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                    }
                    else
                    {
                        CartDetailsManager.DeleteOneEntry(IdCartDetails);
                    }

                    cartDetailsList = CartDetailsManager.GetAllCartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                    ConfirmOrder myConfirmOrder = new ConfirmOrder(cartDetailsList, 7);
                    myConfirmOrder.errorCode = 0; 
                    return View(myConfirmOrder);
                }
                else
                {
                    var myDeliveryLocation = LocationManager.GetLocation(city, PostCode);
                    cartDetailsList = CartDetailsManager.GetAllCartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                    var myRestaurant = RestaurantManager.GetRestaurantByID(cartDetailsList[0].IdRestaurant);
                    var myRestaurantLocation = LocationManager.GetLocationByID(myRestaurant.IdLocation);

                    if (myDeliveryLocation.IdRegion != myRestaurantLocation.IdRegion)
                    {

                        cartDetailsList = CartDetailsManager.GetAllCartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));

                        ConfirmOrder myConfirmOrder = new ConfirmOrder(cartDetailsList, 7);
                        myConfirmOrder.errorCode = 1; 
                        return View(myConfirmOrder);
                    }
                    if(deliveryTime < DateTime.Now.AddMinutes(30))
                    {
                        cartDetailsList = CartDetailsManager.GetAllCartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));

                        ConfirmOrder myConfirmOrder = new ConfirmOrder(cartDetailsList, 7);
                        myConfirmOrder.errorCode = 2;
                        return View(myConfirmOrder);
                    }
                    

                    float totalPrice = 0;

                    foreach (var chartDetail in cartDetailsList)
                    {
                        totalPrice += (float)(chartDetail.UnitPrice * chartDetail.Quantity);
                    }

                    var myOrder = new Order(DateTime.Now, deliveryTime, DeliveryAddress, 7, totalPrice, 1, (int)HttpContext.Session.GetInt32("ID_USER"),
                        myDeliveryLocation.IdLocation, cartDetailsList[0].IdRestaurant);


                    var myNewOrder = OrderManager.AddNewOrder(myOrder);
                    foreach (var charDetail in cartDetailsList)
                    {
                        var myOrderDetails = new OrderDetails(charDetail.UnitPrice, charDetail.Quantity, charDetail.IdProduct, myNewOrder.IdOrder);
                        OrderDetailsManager.AddOrderDetails(myOrderDetails);
                    }
                    CartDetailsManager.DeleteAllEntryByLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                }
            
                return RedirectToAction("Index", "Restaurant");
        }
    }
}
