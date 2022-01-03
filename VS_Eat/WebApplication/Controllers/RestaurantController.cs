using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;
using DTO;
using System.Collections.Generic;
using BLL;
using WebApplication.Models;


namespace WebApplication.Controllers 
{
    public class RestaurantController : Controller
    {
        private IRestaurantManager RestaurantManager { get; }
        private IProductManager ProductManager { get; }
        private ILocationManager LocationManager { get; }
        private IUserManager UserManager { get; }
        private IRegionManager RegionManager { get; }
        private ICartDetailsManager CartDetailsManager { get; }
        private IOrderManager OrderManager { get; }
        private IDeliveryStaffManager DeliveryStaffManager { get; }
        private IReviewManager ReviewManager { get; }

        public RestaurantController(IRestaurantManager RestaurantManager, IProductManager ProductManager, ILocationManager LocationManager, IUserManager UserManager, IRegionManager RegionManager,ICartDetailsManager CartDetailsManager, IOrderManager OrderManager, IDeliveryStaffManager DeliveryStaffManager, IReviewManager ReviewManager)
        {
            this.RestaurantManager = RestaurantManager;
            this.ProductManager = ProductManager;
            this.LocationManager = LocationManager;
            this.UserManager = UserManager;
            this.RegionManager = RegionManager;
            this.CartDetailsManager = CartDetailsManager;
            this.OrderManager = OrderManager;
            this.DeliveryStaffManager = DeliveryStaffManager;
            this.ReviewManager = ReviewManager;
            this.RestaurantTypeManager = RestaurantTypeManager; 
        }
        public IActionResult Index()
        {
            
            if(HttpContext.Session.GetInt32("ID_LOGIN") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account"); 
            }
            //list with all restaurant 
            var restaurants = RestaurantManager.GetAllRestaurants();
            
            //new list where we will put all restaurant from a region
            var restaurantsFromMyRegion = new List<Restaurant>();
            var myUser = UserManager.GetUserByID((int)HttpContext.Session.GetInt32("ID_LOGIN"));
            var myLocation = LocationManager.GetLocationByID(myUser.IdLocation);
            var myRegion = RegionManager.GetRegionName(myLocation.IdRegion);
            var myrestaurantTypes = RestaurantTypeManager.GetAllRestaurantType();
            var restauranTypeToDisplay = new List<string>(); 

            foreach(var type in myrestaurantTypes)
            {
                restauranTypeToDisplay.Add(type.NomRestaurantType); 
            }
            foreach (var restaurant in restaurants)
            {
                var location = LocationManager.GetLocationByID(restaurant.IdLocation);
                if (location.IdRegion == myRegion.IdRegion)
                {
                    restaurantsFromMyRegion.Add(restaurant);
                }
            }


            var ReviewsToDisplay = new List<ReviewToDisplay>();
            foreach (var restaurant in restaurantsFromMyRegion)
            {
                var reviews = ReviewManager.GetAllReviewByRestaurantID(restaurant.IdRestaurant);
                var myReviewToDisplay = new ReviewToDisplay();
                myReviewToDisplay.Comment = new List<string>(); 
                myReviewToDisplay.IdRestaurant = restaurant.IdRestaurant;

                foreach (var review in reviews)
                {                  
                    myReviewToDisplay.total += review.Stars;
                    myReviewToDisplay.numberOfReview += 1;
                    if (!(String.IsNullOrEmpty(review.Comment)))
                    {
                        myReviewToDisplay.Comment.Add(review.Comment);
                    }          
                }

                if (myReviewToDisplay.numberOfReview > 0)
                {
                    myReviewToDisplay.average = Math.Round((double)myReviewToDisplay.total / (double) myReviewToDisplay.numberOfReview ,0);
                }

                ReviewsToDisplay.Add(myReviewToDisplay);
            }

            var regions = RegionManager.GetAllRegions();
            RestaurantToDisplay allData = new RestaurantToDisplay();

            allData.myRegion = myRegion; 
            allData.allRestaurant = restaurantsFromMyRegion;
            allData.RegionName = regions;
            allData.AllReview = ReviewsToDisplay;
            allData.AllRestaurantType = myrestaurantTypes;
            allData.AllTypeToDisplay = restauranTypeToDisplay; 
        
            return View(allData);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(string regionName, List<string> restaurantType)
        {
            if (HttpContext.Session.GetInt32("ID_LOGIN") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }
            //list with all restaurant 
            var restaurants = RestaurantManager.GetAllRestaurants();
            var myRegion = new Region() ; 
            //new list where we will put all restaurant from a region
            var restaurantsFromMyRegion = new List<Restaurant>();
            var myrestaurantTypes = RestaurantTypeManager.GetAllRestaurantType();

            if (string.IsNullOrEmpty(regionName))
            {
                var myUser = UserManager.GetUserByID((int)HttpContext.Session.GetInt32("ID_LOGIN"));
                var myLocation = LocationManager.GetLocationByID(myUser.IdLocation);
                myRegion = RegionManager.GetRegionName(myLocation.IdRegion);
            }
            else
            {
                myRegion = RegionManager.GetRegionName(RegionManager.GetIdRegion(regionName));
            }
         

            foreach (var restaurant in restaurants)
            {
                var location = LocationManager.GetLocationByID(restaurant.IdLocation);
                if (location.IdRegion == myRegion.IdRegion)
                {
                    foreach(var type in myrestaurantTypes)
                    {
                        if(restaurantType.Contains(type.NomRestaurantType) && restaurant.IdRestaurantType == type.IdRestaurantType)
                        {
                            restaurantsFromMyRegion.Add(restaurant);
                        }
                    }
                
                }
            }


            var ReviewsToDisplay = new List<ReviewToDisplay>();
            foreach (var restaurant in restaurantsFromMyRegion)
            {
                var reviews = ReviewManager.GetAllReviewByRestaurantID(restaurant.IdRestaurant);
                var myReviewToDisplay = new ReviewToDisplay();
                myReviewToDisplay.Comment = new List<string>();
                myReviewToDisplay.IdRestaurant = restaurant.IdRestaurant;

                foreach (var review in reviews)
                {
                    myReviewToDisplay.total += review.Stars;
                    myReviewToDisplay.numberOfReview += 1;
                    if (!(String.IsNullOrEmpty(review.Comment)))
                    {
                        myReviewToDisplay.Comment.Add(review.Comment);
                    }
                }
                if (myReviewToDisplay.numberOfReview > 0)
                {
                    myReviewToDisplay.average = Math.Round((double)myReviewToDisplay.total / (double)myReviewToDisplay.numberOfReview, 0);
                }


                ReviewsToDisplay.Add(myReviewToDisplay);
            }

            var regions = RegionManager.GetAllRegions();
            RestaurantToDisplay allData = new RestaurantToDisplay();

            allData.myRegion = myRegion;
            allData.allRestaurant = restaurantsFromMyRegion;
            allData.RegionName = regions;
            allData.AllReview = ReviewsToDisplay;
            allData.AllRestaurantType = myrestaurantTypes;
            allData.AllTypeToDisplay = restaurantType; 


            return View(allData);

        }

        public ActionResult ShowAllProductFromRestaurant(int id)
        {
            var products = ProductManager.GetAllProductsFromRestaurant(id);

            AllProductWithCart myPage = new AllProductWithCart();
            myPage.myCart = CartDetailsManager.GetAllCartDetailsFromLogin((int)HttpContext.Session.GetInt32("ID_LOGIN"));
            myPage.products = products;
            myPage.IdRestaurant = id;

            List<string> comments = new List<string>();
            comments = ReviewManager.GetAllCommentByRestaurantID(id);
            myPage.Comment = comments;

            return View(myPage); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowAllProductFromRestaurant(int IdProduct, string ProductName,string ProductImage, int Quantity, float Price, int IdRestaurant)
        {
            int IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");

            var products = new List<Product>();
            AllProductWithCart myPage = new AllProductWithCart();

            CartDetails myCartDetails = new CartDetails();

            List<string> comments = new List<string>();
            comments = ReviewManager.GetAllCommentByRestaurantID(IdRestaurant); 
            myCartDetails.IdLogin = IdLogin;
            myCartDetails.IdProduct = IdProduct;
            myCartDetails.IdRestaurant = IdRestaurant;
            myCartDetails.ProductName = ProductName;
            myCartDetails.ProductImage = ProductImage; 
            myCartDetails.Quantity = Quantity;
            myCartDetails.UnitPrice = (float)Price; 

            //Création d'une nouvelle ligne dans la base de donnée avec la nouvelle information du panier 
            CartDetailsManager.AddNewCartDetails(myCartDetails);

            products = ProductManager.GetAllProductsFromRestaurant(IdRestaurant);

            myPage.myCart = CartDetailsManager.GetAllCartDetailsFromLogin(IdLogin);
            myPage.products = products;
            myPage.IdRestaurant = IdRestaurant;
            myPage.Comment = comments; 

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
                        testBefore += 1; 
                    

                    if(order.DeliveryTime >= myOrder.DeliveryTime && order.DeliveryTime <= timeControlAfter)
                        testAfter += 1; 
                    

                    if(order.DeliveryTime >= timeControleBetween && order.DeliveryTime <= timeControleBetween2)
                        testBetween += 1;

                }
                if(testAfter < 5 && testBetween < 5 && testBefore < 5)
                    StaffAvailable.Add(staff);
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
