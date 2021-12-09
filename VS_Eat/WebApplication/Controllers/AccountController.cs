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
        private IUserManager UserManager { get;  }
        private ILoginManager LoginManager { get;  }

        private ILocationManager LocationManager { get;  }
        public AccountController(IUserManager UserManager, ILoginManager LoginManager, ILocationManager LocationManager)
        {
            this.LoginManager = LoginManager;
            this.UserManager = UserManager;
            this.LocationManager = LocationManager; 
        }
        public ActionResult CreateAnAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAnAccount(Login_User login_User)
        {
            User myNewUser = UserManager.CreateNewUser(login_User.FirstName, login_User.LastName, login_User.PhoneNumber, login_User.Address, login_User.Username, login_User.Password, login_User.PostCode, login_User.City);
            if(myNewUser == null)
            {
                //TODO create a page to handle doublon ( redirect to login page ?) 
                return RedirectToAction("login", "Account"); 
            }
           
            return RedirectToAction("Login", "Account"); 
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login myLogin)
        {
            Login myAccount = LoginManager.GetLoginWithCredential(myLogin.Username, myLogin.Password);
            if (myAccount != null)
            {
                HttpContext.Session.SetInt32("ID_LOGIN", myAccount.IdLogin);
                var myUser = UserManager.GetUserByID(myAccount.IdLogin);
                HttpContext.Session.SetInt32("ID_USER", myUser.IdUser); 
                return RedirectToAction("Index", "Restaurant");
            }

            return View(); 
        }


        public ActionResult AccountInformation()
        {     
            int IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");

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
    
        public ActionResult EditAccount()
        {
            int IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");

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
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAccount(Login_User login_user)
        {
            var myUser = new User();
            var myLogin = new Login();
            var myLocation = LocationManager.GetLocation(login_user.City, login_user.PostCode); 

            myUser.IdUser = (int)HttpContext.Session.GetInt32("ID_USER");
            myUser.IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");
            myUser.FirstName = login_user.FirstName;
            myUser.LastName = login_user.LastName;
            myUser.PhoneNumber = login_user.PhoneNumber;
            myUser.Address = login_user.Address;
            myUser.IdLocation = myLocation.IdLocation; 


            myLogin.IdLogin = (int)HttpContext.Session.GetInt32("ID_LOGIN");
            myLogin.Username = login_user.Username;
            myLogin.Password = login_user.Password;

            LoginManager.UpdateLogin(myLogin);
            UserManager.UpdateUser(myUser);

            return RedirectToAction("AccountInformation", "Account"); 
        }
    
    }
}
