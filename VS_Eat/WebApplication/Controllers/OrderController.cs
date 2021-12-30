using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private IOrderManager OrderManager { get; }
        private IOrderDetailsManager OrderDetailsManager { get; }

        private IChartDetailsManager ChartDetailsManager { get;  }

        private ILocationManager LocationManager { get;  }

        private IRestaurantManager RestaurantManager { get; }
        public OrderController(IOrderManager OrderManager, IOrderDetailsManager orderDetailsManager, IChartDetailsManager ChartDetailsManager, ILocationManager LocationManager, IRestaurantManager RestaurantManager)
        {
            this.OrderManager = OrderManager;
            this.OrderDetailsManager = orderDetailsManager;
            this.ChartDetailsManager = ChartDetailsManager;
            this.LocationManager = LocationManager;
            this.RestaurantManager = RestaurantManager; 
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


        public IActionResult ConfirmOrder()
        {
            var myChartDetails = ChartDetailsManager.GetAllChartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
            return View(myChartDetails); 
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult ConfirmOrder(string DeliveryAddress, string city, int PostCode, int IdChartDetails)
        {
            var myChartDetails = new List<ChartDetails>();
 
                if (IdChartDetails != 0)
                {
                    ChartDetailsManager.DeleteOneEntry(IdChartDetails);

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
                    myOrder.DeliveryTime = DateTime.Now.AddMinutes(30);
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
