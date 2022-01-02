﻿using BLL.Interfaces;
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

        private IChartDetailsManager ChartDetailsManager { get;  }

        private ILocationManager LocationManager { get;  }

        private IRestaurantManager RestaurantManager { get; }
        public OrderController(IOrderManager OrderManager, IOrderDetailsManager orderDetailsManager, IChartDetailsManager ChartDetailsManager, ILocationManager LocationManager, IRestaurantManager RestaurantManager, IOrderStatusManager OrderStatusManager)
        {
            this.OrderManager = OrderManager;
            this.OrderDetailsManager = orderDetailsManager;
            this.ChartDetailsManager = ChartDetailsManager;
            this.LocationManager = LocationManager;
            this.RestaurantManager = RestaurantManager;
            this.OrderStatusManager = OrderStatusManager; 
        }
        public IActionResult ShowAllOrders()
        {
            List<OrdersList> ordersList = new List<OrdersList>();
            var orders = OrderManager.GetOrderByUser((int)HttpContext.Session.GetInt32("ID_USER"));
            
            foreach(var order in orders)
            {
                OrdersList myOrderList = new OrdersList();
                var myLocation = LocationManager.GetLocationByID(order.IdLocation);
                var mystatus = OrderStatusManager.GetOrderStatus(order.IdOrderStatus);
                var myRestaurant = RestaurantManager.GetRestaurantByID(order.IdRestaurant); 

                myOrderList.IdOrder = order.IdOrder;
                myOrderList.DeliveryTime = order.DeliveryTime;
                myOrderList.DeliveryAddress = order.DeliveryAddress;
                // aucun nom n'est spécifié car on affiche les commandes d'un user donc le nom est logiquement toujours le même
                myOrderList.RecipientFirstName = "";
                myOrderList.RecipientLastName = "";
                myOrderList.Postcode = myLocation.PostCode;
                myOrderList.City = myLocation.City;
                myOrderList.TotalPrice = order.TotalPrice;
                myOrderList.IdOrderStatus = order.IdOrderStatus;
                myOrderList.OrderStatus = mystatus.Status;
                myOrderList.RestaurantName = myRestaurant.RestaurantName;
                myOrderList.RestaurantAddress = myRestaurant.RestaurantAddress;

                ordersList.Add(myOrderList); 
            }
            return View(ordersList);
        }

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
                OrdersList myOrderList = new OrdersList();
                var myLocation = LocationManager.GetLocationByID(order.IdLocation);
                var mystatus = OrderStatusManager.GetOrderStatus(order.IdOrder);
                var myRestaurant = RestaurantManager.GetRestaurantByID(order.IdRestaurant);

                myOrderList.IdOrder = order.IdOrder;
                myOrderList.DeliveryTime = order.DeliveryTime;
                myOrderList.DeliveryAddress = order.DeliveryAddress;
                // aucun nom n'est spécifié car on affiche les commandes d'un user donc le nom est logiquement toujours le même
                myOrderList.RecipientFirstName = "";
                myOrderList.RecipientLastName = "";
                myOrderList.Postcode = myLocation.PostCode;
                myOrderList.City = myLocation.City;
                myOrderList.TotalPrice = order.TotalPrice;
                myOrderList.IdOrderStatus = order.IdOrderStatus;
                myOrderList.OrderStatus = mystatus.Status;
                myOrderList.RestaurantName = myRestaurant.RestaurantName;
                myOrderList.RestaurantAddress = myRestaurant.RestaurantAddress;
                ordersList.Add(myOrderList);
            }
            return View(ordersList);
        }

        public IActionResult ShowOrderDetail(int IdOrder)
        {
            var orderDetails = OrderDetailsManager.GetOrderDetailsFromOrder(IdOrder); 
            return View(orderDetails); 
        }


        public IActionResult ConfirmOrder()
        {
            var myChartDetails = ChartDetailsManager.GetAllChartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
            return View(myChartDetails); 
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult ConfirmOrder(string DeliveryAddress, string city, int PostCode, int IdChartDetails,DateTime deliveryTime)
        {
            var myChartDetails = new List<ChartDetails>();
 
                if (IdChartDetails != 0)
                {
                    if(IdChartDetails == -1)
                    {
                    ChartDetailsManager.DeleteAllEntryByLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                    }
                    else {
                    ChartDetailsManager.DeleteOneEntry(IdChartDetails);
                    }
                    

                    myChartDetails = ChartDetailsManager.GetAllChartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                    return View(myChartDetails);
                }
                else
                {


                    var myDeliveryLocation = LocationManager.GetLocation(city, PostCode);
                    myChartDetails = ChartDetailsManager.GetAllChartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                    var myRestaurant = RestaurantManager.GetRestaurantByID(myChartDetails[0].IdRestaurant);
                    var myRestaurantLocation = LocationManager.GetLocationByID(myRestaurant.IdLocation);

                    if (myDeliveryLocation.IdRegion != myRestaurantLocation.IdRegion)
                    {
                        if (ModelState.IsValid)
                        {
                        
                      
                            ModelState.AddModelError(string.Empty, "The address delivery region's must be the same as the restaurant region !");
                        }
                        myChartDetails = ChartDetailsManager.GetAllChartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                        return View(myChartDetails);
                    }

                    var myOrder = new Order();
                    float totalPrice = 0;
                    foreach (var chartDetail in myChartDetails)
                    {
                        totalPrice += (float)(chartDetail.UnitPrice * chartDetail.Quantity);
                    }                 
                    myOrder.OrderDate = DateTime.Now;          
             
                    myOrder.DeliveryTime = deliveryTime;
                    myOrder.DeliveryAddress = DeliveryAddress;
                    myOrder.Freight = 10;
                    myOrder.TotalPrice = totalPrice;
                    myOrder.IdOrderStatus = 1;
                    myOrder.IdUser = (int)HttpContext.Session.GetInt32("ID_USER");
                    myOrder.IdLocation = myDeliveryLocation.IdLocation;
                    myOrder.IdRestaurant = myChartDetails[0].IdRestaurant; 

                    var myNewOrder = OrderManager.AddNewOrder(myOrder);
                    foreach (var charDetail in myChartDetails)
                    {
                        var myOrderDetails = new OrderDetails();
                        myOrderDetails.IdOrder = myNewOrder.IdOrder;
                        myOrderDetails.IdProduct = charDetail.IdProduct;
                        myOrderDetails.Quantity = charDetail.Quantity;
                        myOrderDetails.UnitPrice = charDetail.UnitPrice;


                        OrderDetailsManager.AddOrderDetails(myOrderDetails);
                    }
                    ChartDetailsManager.DeleteAllEntryByLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));



            
                }
            
            return RedirectToAction("Index", "Restaurant");
        }
    }
}
