using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private IUserManager UserManager { get;  }

        public AccountController(IUserManager UserManager)
        {
            this.UserManager = UserManager; 
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

        public ActionResult ShowUserInformation(User myUser)
        {
            return View(myUser); 
        }
    }
}
