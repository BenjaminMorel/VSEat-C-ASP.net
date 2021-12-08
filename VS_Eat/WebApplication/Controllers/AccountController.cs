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
           
            return RedirectToAction("ShowUserInformation", "Account",myNewUser); 
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
                HttpContext.Session.SetInt32("ID", myAccount.IdLogin); 
                return RedirectToAction("Index", "Restaurant");
            }

            return View(); 
        }

        public ActionResult ShowUserInformation(User myUser)
        {
            return View(myUser); 
        }

        public ActionResult AccountInformation()
        {     
            int IdLogin = (int)HttpContext.Session.GetInt32("ID");

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
    }
}
