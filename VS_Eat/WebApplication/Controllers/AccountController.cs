using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager UserManager { get; }
        private ILoginManager LoginManager { get; }
        private ILocationManager LocationManager { get; }
        private IDeliveryStaffManager DeliveryStaffManager { get; }
        private IRestaurantManager RestaurantManager { get;  }
        private IRegionManager RegionManager { get; }

        public AccountController(IUserManager UserManager, ILoginManager LoginManager, ILocationManager LocationManager,
            IDeliveryStaffManager DeliveryStaffManager, IRegionManager RegionManager, IRestaurantManager RestaurantManager)
        {
            this.LoginManager = LoginManager;
            this.UserManager = UserManager;
            this.LocationManager = LocationManager;
            this.DeliveryStaffManager = DeliveryStaffManager;
            this.RegionManager = RegionManager;
            this.RestaurantManager = RestaurantManager; 
        }

        /// <summary>
        /// Method to display the page where you can create a user account
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateAnAccount()
        {
            return View();
        }
        

        /// <summary>
        /// Http Post method that take the new created user as a parameter and add it to the database 
        /// </summary>
        /// <param name="login_User"> the new user that has been created</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAnAccount(Login_User login_User)
        {
            User myNewUser = UserManager.CreateNewUser(login_User.FirstName, login_User.LastName,
                login_User.PhoneNumber, login_User.Address, login_User.Username, login_User.Password,
                login_User.PostCode, login_User.City);
            
            if (myNewUser == null)
            {

                //TODO create a page to handle doublon ( redirect to login page ?) 
                return RedirectToAction("login", "Account");
            }

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Methode to display the page where you can create a new Delivery staff account
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateDeliveryStaff()
        {
            var AllRegions = RegionManager.GetAllRegions();
            return View();
        }


        /// <summary>
        /// Http post method that take the new created staff as a parameter and add it to the delivery staff database
        /// </summary>
        /// <param name="login_DeliveryStaff">the new staff member that has been created</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDeliveryStaff(Login_DeliveryStaff login_DeliveryStaff)
        {
            DeliveryStaff myDeliveryStaff = DeliveryStaffManager.CreateNewStaff(login_DeliveryStaff.FirstName,
                login_DeliveryStaff.LastName, login_DeliveryStaff.PhoneNumber, login_DeliveryStaff.Address, login_DeliveryStaff.PostCode,
                login_DeliveryStaff.City, login_DeliveryStaff.RegionName, login_DeliveryStaff.EmailAddress, login_DeliveryStaff.Password);
            
            if (myDeliveryStaff == null)
            {
                //TODO create a page to handle doublon ( redirect to login page ?) 
                return RedirectToAction("login", "Account");
            }

            return RedirectToAction("Login", "Account");
        } 
        
        /// <summary>
        /// method to display the page where you could login in the application (as user / staff /admin or restaurant) 
        /// </summary>
        /// <returns></returns>
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }


        /// <summary>
        ///Http post method that take the credentials and verified them 
        /// </summary>
        /// <param name="myLogin">The cretendial that have been enter by the user into the login form</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login myLogin)
        {      
            Login myAccount = LoginManager.GetLoginWithCredential(myLogin.Username, myLogin.Password);

            if (ModelState.IsValid)
            {
                if (myAccount != null)
                {
                    HttpContext.Session.SetInt32("ID_LOGIN", myAccount.IdLogin);

                    if (myAccount.IdLoginType == 2)
                    {
                        var myRestaurant = RestaurantManager.GetRestaurantByIDLogin(myAccount.IdLogin);
                        HttpContext.Session.SetInt32("ID_RESTAURANT", myRestaurant.IdRestaurant);
                        return RedirectToAction("MainPageRestaurant", "Restaurant");
                    }

                    if (myAccount.IdLoginType == 3)
                    {
                        var myDeliveryStaff = DeliveryStaffManager.GetDeliveryStaffByID(myAccount.IdLogin);
                        HttpContext.Session.SetInt32("ID_STAFF", myDeliveryStaff.IdDeliveryStaff);
                        return RedirectToAction("Index", "DeliveryStaff");
                    }

                    if (myAccount.IdLoginType == 4)
                    {
                        var myUser = UserManager.GetUserByID(myAccount.IdLogin);
                        HttpContext.Session.SetInt32("ID_USER", myUser.IdUser);
                        return RedirectToAction("Index", "Restaurant");
                    }

                    if (myAccount.IdLoginType == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }


                    return View();

                }

                ModelState.AddModelError(string.Empty, "Invalid Email or password");
            }
            return View();
        }

        /// <summary>
        /// Method to display the page where you could see your personnal information 
        /// </summary>
        /// <returns></returns>
        public IActionResult AccountInformation()
        {
            int IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");

            var myLogin = LoginManager.GetLoginByID(IdLogin);
            var myUser = UserManager.GetUserByID(IdLogin);

            var myLocation = LocationManager.GetLocationByID(myUser.IdLocation);

            var myLogin_User = new Login_User(myLogin.Username,myLogin.Password,myUser.FirstName,myUser.LastName,myUser.PhoneNumber,myLocation.PostCode,myLocation.City,myUser.Address);

            return View(myLogin_User);
        }

        /// <summary>
        /// Method to display the page where staff can see their personnal infomation 
        /// </summary>
        /// <returns></returns>
        public IActionResult AccountInformationStaff()
        {
            int IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");

            var myLogin = LoginManager.GetLoginByID(IdLogin);
            var myDeliveryStaff = DeliveryStaffManager.GetDeliveryStaffByID(IdLogin);

            var myLocation = LocationManager.GetLocationByID(myDeliveryStaff.IdLocation);
            var myRegion = RegionManager.GetRegionName(myDeliveryStaff.IdWorkingRegion);

            var myLogin_DeliveryStaff = new Login_DeliveryStaff(myLogin.Username,myLogin.Password,myDeliveryStaff.FirstName,myDeliveryStaff.LastName,myDeliveryStaff.PhoneNumber,myDeliveryStaff.Address,myLocation.PostCode,myLocation.City,myRegion.RegionName);

            return View(myLogin_DeliveryStaff);
        }

        /// <summary>
        /// Method to display the page where you can edit your personnal information 
        /// </summary>
        /// <returns></returns>
        public IActionResult EditAccount()
        {
            int IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");

            var myLogin = LoginManager.GetLoginByID(IdLogin);
            var myUser = UserManager.GetUserByID(IdLogin);

            var myLocation = LocationManager.GetLocationByID(myUser.IdLocation);

            var myLogin_User = new Login_User(myLogin.Username, myLogin.Password, myUser.FirstName, myUser.LastName, myUser.PhoneNumber, myLocation.PostCode, myLocation.City, myUser.Address);

            return View(myLogin_User);
        }

        /// <summary>
        /// Http post method to send the modification you made about your personnal information
        /// </summary>
        /// <param name="login_user">the personnal information (old or new) that have been made in the page and that will be change in the database</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAccount(Login_User login_user)
        {

            var myLocation = LocationManager.GetLocation(login_user.City, login_user.PostCode);

            var myUser = new User((int)HttpContext.Session.GetInt32("ID_USER"), login_user.FirstName, login_user.LastName, login_user.PhoneNumber, login_user.Address,
                                 (int)HttpContext.Session.GetInt32("ID_LOGIN"), myLocation.IdLocation);
            var myLogin = new Login((int)HttpContext.Session.GetInt32("ID_LOGIN"), login_user.Username, login_user.Password);

            LoginManager.UpdateLogin(myLogin);
            UserManager.UpdateUser(myUser);

            return RedirectToAction("AccountInformation", "Account");
        }

        /// <summary>
        /// Method to display the page where a staff member can modify his personnal information 
        /// </summary>
        /// <returns></returns>
        public IActionResult EditAccountStaff()
        {
            int IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");

            var myLogin = LoginManager.GetLoginByID(IdLogin);
            var myDeliveryStaff = DeliveryStaffManager.GetDeliveryStaffByID(IdLogin);

            var myLocation = LocationManager.GetLocationByID(myDeliveryStaff.IdLocation);
            var myRegion = RegionManager.GetRegionName(myDeliveryStaff.IdWorkingRegion);

            var myLogin_DeliveryStaff = new Login_DeliveryStaff(myLogin.Username, myLogin.Password, myDeliveryStaff.FirstName, myDeliveryStaff.LastName, myDeliveryStaff.PhoneNumber, myDeliveryStaff.Address, myLocation.PostCode, myLocation.City, myRegion.RegionName);

            return View(myLogin_DeliveryStaff);
        }

        /// <summary>
        /// Http post method that take the new information of the staff member
        /// </summary>
        /// <param name="login_DeliveryStaff">all informations that will be write into the database about the staff member</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditAccountStaff(Login_DeliveryStaff login_DeliveryStaff)
        {
            
            var myLogin = new Login((int)HttpContext.Session.GetInt32("ID_LOGIN"), login_DeliveryStaff.EmailAddress, login_DeliveryStaff.Password);
            var myLocation = LocationManager.GetLocation(login_DeliveryStaff.City, login_DeliveryStaff.PostCode);
            var myRegion = RegionManager.GetIdRegion(login_DeliveryStaff.RegionName);

            var myDeliveryStaff = new DeliveryStaff((int)HttpContext.Session.GetInt32("ID_STAFF"),login_DeliveryStaff.FirstName,login_DeliveryStaff.LastName,login_DeliveryStaff.PhoneNumber,login_DeliveryStaff.Address,
                                       (int)HttpContext.Session.GetInt32("ID_LOGIN"),myLocation.IdLocation,myRegion);

            LoginManager.UpdateLogin(myLogin);
            DeliveryStaffManager.UpdateDeliveryStaff(myDeliveryStaff);

            return RedirectToAction("AccountInformationStaff", "Account");
        }

    }
    
}
