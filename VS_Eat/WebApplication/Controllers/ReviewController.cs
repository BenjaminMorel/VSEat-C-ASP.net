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
    public class ReviewController : Controller
    {
        private IReviewManager ReviewManager { get;  }

        private IRestaurantManager RestaurantManager { get;  }

        public ReviewController(IReviewManager reviewManager, IRestaurantManager RestaurantManager)
        {
            this.ReviewManager = reviewManager;
            this.RestaurantManager = RestaurantManager; 
        }
        public IActionResult AddReview(int IdRestaurant)
        {
            var myRestaurant = RestaurantManager.GetRestaurantByID(IdRestaurant);
            var reviewToAdd = new AddReview();
            reviewToAdd.IdRestaurant = IdRestaurant;
            reviewToAdd.RestaurantName = myRestaurant.RestaurantName;
            return View(reviewToAdd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview(AddReview myNewReview)
        {           
                var myReview = new Review();
                myReview.IdRestaurant = myNewReview.IdRestaurant;
                myReview.Stars = myNewReview.Stars;
                if (!(String.IsNullOrEmpty(myNewReview.Comment)))
                {
                    myReview.Comment = myNewReview.Comment;
                }
                ReviewManager.AddAReview(myReview);

                return RedirectToAction("Index", "Restaurant");
            
        }

    }
}
