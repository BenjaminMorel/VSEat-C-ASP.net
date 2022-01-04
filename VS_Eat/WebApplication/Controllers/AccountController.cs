using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
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

        public ActionResult CreateAnAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnAccount(Login_User login_User)
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

        public ActionResult CreateDeliveryStaff()
        {
            var AllRegions = RegionManager.GetAllRegions();
            Login_DeliveryStaff loginDelivery = new Login_DeliveryStaff();
            loginDelivery.AllRegions = AllRegions;
            return View(loginDelivery);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDeliveryStaff(Login_DeliveryStaff login_DeliveryStaff, string regionName)
        {
            DeliveryStaff myDeliveryStaff = DeliveryStaffManager.CreateNewStaff(login_DeliveryStaff.FirstName,
                login_DeliveryStaff.LastName, login_DeliveryStaff.PhoneNumber, login_DeliveryStaff.Address, login_DeliveryStaff.PostCode,
                login_DeliveryStaff.City, regionName, login_DeliveryStaff.EmailAddress, login_DeliveryStaff.Password);
            
            if (myDeliveryStaff == null)
            {
                //TODO create a page to handle doublon ( redirect to login page ?) 
                return RedirectToAction("login", "Account");
            }

            return RedirectToAction("Login", "Account");
        } 
        

        public ActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login myLogin)
        {      
            Login myAccount = LoginManager.GetLoginWithCredential(myLogin.Username, myLogin.Password);

            if (ModelState.IsValid)
            {
                if (myAccount != null)
                {
                    HttpContext.Session.SetInt32("ID_LOGIN", myAccount.IdLogin);

                    if (myAccount.IdLoginType == 4)
                    {
                        var myUser = UserManager.GetUserByID(myAccount.IdLogin);
                        HttpContext.Session.SetInt32("ID_USER", myUser.IdUser);
                        return RedirectToAction("Index", "Restaurant");
                    }

                    if (myAccount.IdLoginType == 2)
                    {
                        var myRestaurant = RestaurantManager.GetRestaurantByIDLogin(myAccount.IdLogin);
                        HttpContext.Session.SetInt32("ID_RESTAURANT", myRestaurant.IdRestaurant);
                        return RedirectToAction("MainPageRestaurant", "Restaurant");
                    }

                    if (myAccount.IdLoginType == 3)
                    {
                        var myDeliveryStaff = DeliveryStaffManager.GetDeliveryStaffByID(myAccount.IdLogin);

                        if (myDeliveryStaff.IdDeliveryStaffType == 2)
                        {
                            HttpContext.Session.SetInt32("ID_STAFF", myDeliveryStaff.IdDeliveryStaff);
                            return RedirectToAction("Index", "DeliveryStaff");
                        }

                        if (myDeliveryStaff.IdDeliveryStaffType == 1)
                        {
                            ModelState.AddModelError(string.Empty, "Your account is not active for the moment, please wait the validation");
                        }

                        if (myDeliveryStaff.IdDeliveryStaffType == 3)
                        {
                            ModelState.AddModelError(string.Empty, "You have no more access to your account");
                        }

                    }

                    if (myAccount.IdLoginType == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }

                    return View();

                }
                ModelState.AddModelError(string.Empty, "Invalid email or password");

            }
            return View();
        }


        public ActionResult AccountInformation()
        {
            int IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");

            var myLogin = LoginManager.GetLoginByID(IdLogin);
            var myUser = UserManager.GetUserByID(IdLogin);

            var myLocation = LocationManager.GetLocationByID(myUser.IdLocation);

            var myLogin_User = new Login_User();

            myLogin_User.Username = myLogin.Username;
            myLogin_User.Password = myLogin.Password;

            myLogin_User.FirstName = myUser.FirstName;
            myLogin_User.LastName = myUser.LastName;
            myLogin_User.PhoneNumber = myUser.PhoneNumber;
            myLogin_User.Address = myUser.Address;

            myLogin_User.PostCode = myLocation.PostCode;
            myLogin_User.City = myLocation.City;

            return View(myLogin_User);
        }

        public ActionResult AccountInformationStaff()
        {
            int IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");

            var myLogin = LoginManager.GetLoginByID(IdLogin);
            var myDeliveryStaff = DeliveryStaffManager.GetDeliveryStaffByID(IdLogin);

            var myLocation = LocationManager.GetLocationByID(myDeliveryStaff.IdLocation);
            var myRegion = RegionManager.GetRegionName(myDeliveryStaff.IdWorkingRegion);

            var myLogin_DeliveryStaff = new Login_DeliveryStaff();

            myLogin_DeliveryStaff.EmailAddress = myLogin.Username;
            myLogin_DeliveryStaff.Password = myLogin.Password;

            myLogin_DeliveryStaff.FirstName = myDeliveryStaff.FirstName;
            myLogin_DeliveryStaff.LastName = myDeliveryStaff.LastName;
            myLogin_DeliveryStaff.PhoneNumber = myDeliveryStaff.PhoneNumber;
            myLogin_DeliveryStaff.Address = myDeliveryStaff.Address;

            myLogin_DeliveryStaff.RegionName = myRegion.RegionName;

            myLogin_DeliveryStaff.PostCode = myLocation.PostCode;
            myLogin_DeliveryStaff.City = myLocation.City;

            return View(myLogin_DeliveryStaff);
        }

        public ActionResult EditAccount()
        {
            int IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");

            var myLogin = LoginManager.GetLoginByID(IdLogin);
            var myUser = UserManager.GetUserByID(IdLogin);

            var myLocation = LocationManager.GetLocationByID(myUser.IdLocation);

            var myLogin_User = new Login_User();

            myLogin_User.Username = myLogin.Username;
            myLogin_User.Password = myLogin.Password;

            myLogin_User.FirstName = myUser.FirstName;
            myLogin_User.LastName = myUser.LastName;
            myLogin_User.PhoneNumber = myUser.PhoneNumber;
            myLogin_User.Address = myUser.Address;

            myLogin_User.PostCode = myLocation.PostCode;
            myLogin_User.City = myLocation.City;

            return View(myLogin_User);
        }

        public ActionResult EditAccountStaff()
        {
            int IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");

            var myLogin = LoginManager.GetLoginByID(IdLogin);
            var myDeliveryStaff = DeliveryStaffManager.GetDeliveryStaffByID(IdLogin);

            var myLocation = LocationManager.GetLocationByID(myDeliveryStaff.IdLocation);
            var myRegion = RegionManager.GetRegionName(myDeliveryStaff.IdWorkingRegion);

            var myLogin_DeliveryStaff = new Login_DeliveryStaff();

            myLogin_DeliveryStaff.EmailAddress = myLogin.Username;
            myLogin_DeliveryStaff.Password = myLogin.Password;

            myLogin_DeliveryStaff.FirstName = myDeliveryStaff.FirstName;
            myLogin_DeliveryStaff.LastName = myDeliveryStaff.LastName;
            myLogin_DeliveryStaff.PhoneNumber = myDeliveryStaff.PhoneNumber;
            myLogin_DeliveryStaff.Address = myDeliveryStaff.Address;

            myLogin_DeliveryStaff.RegionName = myRegion.RegionName;

            myLogin_DeliveryStaff.PostCode = myLocation.PostCode;
            myLogin_DeliveryStaff.City = myLocation.City;

            return View(myLogin_DeliveryStaff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(Login_User login_user)
        {
            var myUser = new User();
            var myLogin = new Login();
            var myLocation = LocationManager.GetLocation(login_user.City, login_user.PostCode);

            myUser.IdUser = (int) HttpContext.Session.GetInt32("ID_USER");
            myUser.IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");
            myUser.FirstName = login_user.FirstName;
            myUser.LastName = login_user.LastName;
            myUser.PhoneNumber = login_user.PhoneNumber;
            myUser.Address = login_user.Address;
            myUser.IdLocation = myLocation.IdLocation;

            myLogin.IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");
            myLogin.Username = login_user.Username;
            myLogin.Password = login_user.Password;

            LoginManager.UpdateLogin(myLogin);
            UserManager.UpdateUser(myUser);

            return RedirectToAction("AccountInformation", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccountStaff(Login_DeliveryStaff login_DeliveryStaff)
        {
            var myDeliveryStaff = new DeliveryStaff();
            var myLogin = new Login();
            var myLocation = LocationManager.GetLocation(login_DeliveryStaff.City, login_DeliveryStaff.PostCode);
            var myRegion = RegionManager.GetIdRegion(login_DeliveryStaff.RegionName);

            myDeliveryStaff.IdDeliveryStaff = (int) HttpContext.Session.GetInt32("ID_STAFF");
            myDeliveryStaff.IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");
            myDeliveryStaff.FirstName = login_DeliveryStaff.FirstName;
            myDeliveryStaff.LastName = login_DeliveryStaff.LastName;
            myDeliveryStaff.PhoneNumber = login_DeliveryStaff.PhoneNumber;
            myDeliveryStaff.Address = login_DeliveryStaff.Address;
            myDeliveryStaff.IdLocation = myLocation.IdLocation;
            myDeliveryStaff.IdWorkingRegion = myRegion;

            myLogin.IdLogin = (int) HttpContext.Session.GetInt32("ID_LOGIN");
            myLogin.Username = login_DeliveryStaff.EmailAddress;
            myLogin.Password = login_DeliveryStaff.Password;

            LoginManager.UpdateLogin(myLogin);
            DeliveryStaffManager.UpdateDeliveryStaff(myDeliveryStaff);

            return RedirectToAction("AccountInformationStaff", "Account");
        }

    }
    
}
