using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using DTO;
using System.Collections.Generic;
using WebApplication.Models;
using Microsoft.JSInterop;
using javax.jws;

namespace WebApplication.Controllers 
{
    public class RestaurantController : Controller
    {
        private IRestaurantManager RestaurantManager { get; }
        private IProductManager ProductManager { get; }
        private ILocationManager LocationManager { get; }

        private IUserManager UserManager { get; }

        private IRegionManager RegionManager { get; }

        private IChartDetailsManager ChartDetailsManager { get;  }

        private IOrderManager OrderManager { get; }

        private IDeliveryStaffManager DeliveryStaffManager { get;  }



        public RestaurantController(IRestaurantManager RestaurantManager, IProductManager ProductManager, ILocationManager LocationManager, IUserManager UserManager, IRegionManager RegionManager,IChartDetailsManager ChartDetailsManager, IOrderManager OrderManager, IDeliveryStaffManager DeliveryStaffManager)
        {
            this.RestaurantManager = RestaurantManager;
            this.ProductManager = ProductManager;
            this.LocationManager = LocationManager;
            this.UserManager = UserManager;
            this.RegionManager = RegionManager;
            this.ChartDetailsManager = ChartDetailsManager;
            this.OrderManager = OrderManager;
            this.DeliveryStaffManager = DeliveryStaffManager; 
        }
        public ActionResult Index()
        {
            if(HttpContext.Session.GetInt32("ID_LOGIN") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account"); 
            }
            var restaurants = RestaurantManager.GetAllRestaurants();
            var regions = RegionManager.GetAllRegions();
            RestaurantToDisplay allData = new RestaurantToDisplay();

            allData.allRestaurant = restaurants;
            allData.RegionName = regions;
            return View(allData);

        }


        public ActionResult ShowAllProductFromRestaurant(int id)
        {
            var products = ProductManager.GetAllProductsFromRestaurant(id);

            AllProductWithCart myPage = new AllProductWithCart();
            myPage.myChart = ChartDetailsManager.GetAllChartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
            myPage.products = products;
            myPage.IdRestaurant = id; 

            return View(myPage); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowAllProductFromRestaurant(int IdProduct, string ProductName,string ProductImage, int Quantity, float Price, int IdRestaurant)
        {
            int IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");

            var products = new List<Product>();
            AllProductWithCart myPage = new AllProductWithCart();

            ChartDetails myChartDetails = new ChartDetails();
 
            myChartDetails.IdLogin = IdLogin;
            myChartDetails.IdProduct = IdProduct;
            myChartDetails.IdRestaurant = IdRestaurant;
            myChartDetails.ProductName = ProductName;
            myChartDetails.ProductImage = ProductImage; 
            myChartDetails.Quantity = Quantity;
            myChartDetails.UnitPrice = (float)Price; 

            //Création d'une nouvelle ligne dans la base de donnée avec la nouvelle information du panier 
            ChartDetailsManager.AddNewChartDetails(myChartDetails);

            products = ProductManager.GetAllProductsFromRestaurant(IdRestaurant);

            myPage.myChart = ChartDetailsManager.GetAllChartDetailsFromLogin(IdLogin);
            myPage.products = products;
            myPage.IdRestaurant = IdRestaurant;

            return View(myPage); 
        }

        public ActionResult ProductDetails(int id)
        {
            var product = ProductManager.GetProductByID(id);
            Console.WriteLine(product.ToString());
            return View(product); 
        }

        public ActionResult AllRestaurantsByRegion()
        {
            var myUser = UserManager.GetUserByID((int)HttpContext.Session.GetInt32("ID_LOGIN"));
            var User_Location = LocationManager.GetLocationByID(myUser.IdLocation); 
            var restaurants = RestaurantManager.GetAllRestaurants();
            var RestaurantsFromMyRegion = new List<Restaurant>();
            List<Region> region = new List<Region>(); 
            region.Add(RegionManager.GetRegionName(User_Location.IdRegion)); 
            
            var allData = new RestaurantToDisplay();

            foreach (var restaurant in restaurants)
            {
                var location = LocationManager.GetLocationByID(restaurant.IdLocation); 
                if(location.IdRegion == User_Location.IdRegion)
                {
                    RestaurantsFromMyRegion.Add(restaurant); 
                }
            }

    
            allData.allRestaurant = RestaurantsFromMyRegion;
            allData.RegionName = region; 

            return View(allData); 
        }

        public ActionResult MainPageRestaurant()
        {
            var OrderList = OrderManager.GetAllOrderFromRestaurant((int)HttpContext.Session.GetInt32("ID_RESTAURANT")); 
            return View(OrderList); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MainPageRestaurant(int IdOrder)
        {
         
                var myOrder = OrderManager.GetOrderById(IdOrder);


                var myLocation = LocationManager.GetLocationByID(myOrder.IdLocation);
                var OrderList = new List<Order>();
                var StaffInTheRegion = DeliveryStaffManager.FindStaffFororder(myLocation.IdRegion);
                var StaffAvailable = new List<DeliveryStaff>();
      
                foreach (var staff in StaffInTheRegion)
                {
                  
                    var orders = DeliveryStaffManager.CountOrderWithTime(staff.IdDeliveryStaff);
                    int testBefore = 0;
                    int testAfter = 0;
                    int testBetween = 0;
                    var timeControlBefore = myOrder.DeliveryTime.AddMinutes(-30);
                    var timeControlAfter = myOrder.DeliveryTime.AddMinutes(30);
                    var timeControleBetween = myOrder.DeliveryTime.AddMinutes(-15);
                    var timeControleBetween2 = myOrder.DeliveryTime.AddMinutes(15); 
                    foreach (var order in orders)
                        {
                            if(order.DeliveryTime <= myOrder.DeliveryTime && order.DeliveryTime >= timeControlBefore)
                            {
                            testBefore += 1; 
                            }
                            if(order.DeliveryTime >= myOrder.DeliveryTime && order.DeliveryTime <= timeControlAfter)
                            {
                            testAfter += 1; 
                            }
                            if(order.DeliveryTime >= timeControleBetween && order.DeliveryTime <= timeControleBetween2)
                            {
                            testBetween += 1; 
                            }

                        }
                    if(testAfter < 5 && testBetween < 5 && testBefore < 5)
                    {
                    StaffAvailable.Add(staff); 
                    }
                    
                }

                if (StaffAvailable.Count == 0)
                {

                    ModelState.AddModelError(string.Empty, "No staff available now, try again later");
                    OrderList = OrderManager.GetAllOrderFromRestaurant((int)HttpContext.Session.GetInt32("ID_RESTAURANT"));
                    return View(OrderList);


                }
                myOrder.IdDeliveryStaff = StaffAvailable[0].IdDeliveryStaff;
                myOrder.IdOrderStatus = 2;
                OrderManager.AssignStaffToAnOrder(myOrder);
                OrderManager.UpdateOrderStatus(myOrder);
                OrderList = OrderManager.GetAllOrderFromRestaurant((int)HttpContext.Session.GetInt32("ID_RESTAURANT"));
             
            
                return View(OrderList);
        }
       
    }
}
