using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult ContactForm()
        {
            if (HttpContext.Session.GetInt32("ID_LOGIN") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}
