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

        private IRestaurantTypeManager restaurantTypeManager { get; }

        public RestaurantController(IRestaurantManager RestaurantManager, IProductManager ProductManager, ILocationManager LocationManager, IUserManager UserManager, IRegionManager RegionManager,ICartDetailsManager CartDetailsManager, IOrderManager OrderManager, IDeliveryStaffManager DeliveryStaffManager, IReviewManager ReviewManager, IRestaurantTypeManager restaurantTypeManager)
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
            this.restaurantTypeManager = restaurantTypeManager; 
        }
     
        /// <summary>
        /// Method to display all restaurant to a user
        /// This method will create a list of restaurant with every restaurant in the data base and then it will add it to a new list
        /// if the region is the correct one and if the restaurant type match too
        /// it will also create a list of review for every restaurant about his stars rating
        /// </summary>
        /// <returns></returns>
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
            var myrestaurantTypes = restaurantTypeManager.GetAllRestaurantType();
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
            RestaurantToDisplay allData = new RestaurantToDisplay(restaurantsFromMyRegion, myRegion, regions, myrestaurantTypes, restauranTypeToDisplay,ReviewsToDisplay);
        
            return View(allData);

        }


        /// <summary>
        /// http post method that is called when a user want to change the filter he use on the restaurant page
        /// It will display the same page but with a different short of all restaurant 
        /// </summary>
        /// <param name="regionName">The specific region that we want to see</param>
        /// <param name="restaurantType">All type of restaurant that we want to see</param>
        /// <returns></returns>
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
            var myrestaurantTypes = restaurantTypeManager.GetAllRestaurantType();

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
            RestaurantToDisplay allData = new RestaurantToDisplay(restaurantsFromMyRegion, myRegion, regions, myrestaurantTypes, restaurantType, ReviewsToDisplay);

            return View(allData);

        }

        /// <summary>
        /// Method to display the page where the user can see all products from a specific restaurant 
        /// </summary>
        /// <param name="id">The id of the restaurant that we want to show all of his products</param>
        /// <returns></returns>
        public IActionResult ShowAllProductFromRestaurant(int id)
        {

            if (HttpContext.Session.GetInt32("ID_LOGIN") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }

            var products = ProductManager.GetAllProductsFromRestaurant(id);
            int IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");
            var allCartDetails = CartDetailsManager.GetAllCartDetailsFromLogin(IdLogin);
            var myRestaurant = new Restaurant(); 
            if (allCartDetails.Count > 0)
            {
                myRestaurant = RestaurantManager.GetRestaurantByID(allCartDetails[0].IdRestaurant);
            }
        
            List<string> comments = new List<string>();
            comments = ReviewManager.GetAllCommentByRestaurantID(id);
            AllProductWithCart myPage = new AllProductWithCart(products,allCartDetails, id, comments,myRestaurant.RestaurantName);
            return View(myPage); 
        }

        /// <summary>
        /// Http post method to add a new content into the cart 
        /// </summary>
        /// <param name="IdProduct">The id product that will be add into the cart</param>
        /// <param name="ProductName">the name of the product</param>
        /// <param name="ProductImage">the image of the product</param>
        /// <param name="Quantity">the quantity we add into the cart</param>
        /// <param name="Price">the price of the product</param>
        /// <param name="IdRestaurant">the id of the restaurant</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ShowAllProductFromRestaurant(int IdProduct, string ProductName,string ProductImage, int Quantity, float Price, int IdRestaurant,int IdCartDetail)
        {

            bool ISCartUpdate = false; 
            int IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");
            var products = new List<Product>();
          

            if (IdCartDetail != 0)
            {
                CartDetailsManager.DeleteOneEntry(IdCartDetail);
                products = ProductManager.GetAllProductsFromRestaurant(IdRestaurant);
                var allCartDetails = CartDetailsManager.GetAllCartDetailsFromLogin(IdLogin);
                var myRestaurant = new Restaurant();
                if (allCartDetails.Count > 0)
                {
                    myRestaurant = RestaurantManager.GetRestaurantByID(allCartDetails[0].IdRestaurant);
                }

                List<string> comments = new List<string>();
                comments = ReviewManager.GetAllCommentByRestaurantID(IdRestaurant);
                AllProductWithCart myPage = new AllProductWithCart(products, allCartDetails, IdRestaurant, comments, myRestaurant.RestaurantName);
                return View(myPage);
            }
            else
            {

                CartDetails myCartDetails = new CartDetails(IdLogin, IdProduct, IdRestaurant, ProductName, ProductImage, Quantity, Price);
                //vérification si le produit est déjà dans le panier on l'incremente de 1 sinon on le rajoute 
                var allExistingCartDetail = CartDetailsManager.GetAllCartDetailsFromLogin(IdLogin);

                foreach (var product in allExistingCartDetail)
                {
                    if (product.IdProduct == IdProduct)
                    {
                        product.Quantity += Quantity;
                        CartDetailsManager.UpdateQuantity(product);
                        ISCartUpdate = true;
                    }
                }
                //Création d'une nouvelle ligne dans la base de donnée avec la nouvelle information du panier 
                if (!(ISCartUpdate))
                {
                    myCartDetails = CartDetailsManager.AddNewCartDetails(myCartDetails);
                    allExistingCartDetail.Add(myCartDetails);
                }


                var myRestaurant = new Restaurant();
                if (allExistingCartDetail.Count > 0)
                {
                    myRestaurant = RestaurantManager.GetRestaurantByID(allExistingCartDetail[0].IdRestaurant);
                }

                List<string> comments = new List<string>();
                comments = ReviewManager.GetAllCommentByRestaurantID(IdRestaurant);

                products = ProductManager.GetAllProductsFromRestaurant(IdRestaurant);

                AllProductWithCart myPage = new AllProductWithCart(products, allExistingCartDetail, IdRestaurant, comments, myRestaurant.RestaurantName);

                return View(myPage);
            }
        }

        /// <summary>
        /// Method to display the page to show to a restaurant login 
        /// It contain all order that have been passed to his restaurant
        /// </summary>
        /// <returns></returns>
        public IActionResult MainPageRestaurant()
        {

            if (HttpContext.Session.GetInt32("ID_RESTAURANT") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }
            var OrderList = OrderManager.GetAllOrderFromRestaurant((int)HttpContext.Session.GetInt32("ID_RESTAURANT")); 
            return View(OrderList); 
        }

        /// <summary>
        /// Http post method to let a restaurant signal that his order is ready to be delivered by a staff
        /// The controller will verify that a staff is available in the same region 
        /// and it will also verified that the staff didn't had more than 5 order to delivered every 30  min
        /// </summary>
        /// <param name="IdOrder">The id of the order that is now ready</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MainPageRestaurant(int IdOrder)
        {
            var myOrder = OrderManager.GetOrderById(IdOrder);
        
            var myLocation = LocationManager.GetLocationByID(myOrder.IdLocation);
            var OrderList = new List<Order>();
            var StaffInTheRegion = DeliveryStaffManager.FindStaffFororder(myLocation.IdRegion);
            var StaffAvailable = new List<DeliveryStaff>();

            foreach (var staff in StaffInTheRegion)
            {
              
                var orders = DeliveryStaffManager.CountOpenOrderByStaffID(staff.IdDeliveryStaff);
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

        /// <summary>
        /// Method to display the page where the restaurant can see all his products
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowAllProducts()
        {

            if (HttpContext.Session.GetInt32("ID_RESTAURANT") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }
            int IdRestaurant = (int)HttpContext.Session.GetInt32("ID_RESTAURANT");
            var products = ProductManager.GetAllProductsFromRestaurant(IdRestaurant);
            return View(products);
        }

        /// <summary>
        /// Method to display the page where the restaurant can see all his reviews
        /// </summary>
        /// <returns></returns>
        public IActionResult ShowAllReviews()
        {

            if (HttpContext.Session.GetInt32("ID_RESTAURANT") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }
            int IdRestaurant = (int)HttpContext.Session.GetInt32("ID_RESTAURANT");
            var reviews = ReviewManager.GetAllReviewByRestaurantID(IdRestaurant);
            return View(reviews);
        }


    }
}
