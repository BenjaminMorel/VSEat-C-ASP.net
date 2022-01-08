using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using Microsoft.AspNetCore.Http;

namespace WebApplication.Controllers
{
    public class ReviewController : Controller
    {
        private IReviewManager ReviewManager { get;  }

        private IRestaurantManager RestaurantManager { get;  }

        public ReviewController(IReviewManager reviewManager, IRestaurantManager RestaurantManager)
        {
            this.ReviewManager = reviewManager;
            this.RestaurantManager = RestaurantManager; 
        }
        

        /// <summary>
        /// Method to display the page where a use can add a review about a restaraurant
        /// </summary>
        /// <param name="IdRestaurant"></param>
        /// <returns></returns>
        public IActionResult AddReview(int IdRestaurant)
        {

            if (HttpContext.Session.GetInt32("ID_LOGIN") == null)
            {
                //ligne pour forcer la personne a se loger la première fois 
                return RedirectToAction("Login", "Account");
            }
            var myRestaurant = RestaurantManager.GetRestaurantByID(IdRestaurant);
            var reviewToAdd = new AddReview(IdRestaurant,myRestaurant.RestaurantName);
            return View(reviewToAdd);
        }

        /// <summary>
        /// http post method to get the value of the newly add review and insert it into the database
        /// </summary>
        /// <param name="myNewReview">The comment and stars written by the user that we will write into the database</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(AddReview myNewReview)
        {
                var myReview = new Review(myNewReview.IdRestaurant, myNewReview.Stars);
                if (!(String.IsNullOrEmpty(myNewReview.Comment)))
                {
                    myReview.Comment = myNewReview.Comment; 
                }
                else
                {
                    myReview.Comment = ""; 
                }
     
                ReviewManager.AddAReview(myReview);

                return RedirectToAction("Index", "Restaurant");
            
        }

    }
}
