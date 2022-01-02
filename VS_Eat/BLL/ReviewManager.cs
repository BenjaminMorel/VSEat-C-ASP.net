using BLL.Interfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ReviewManager : IReviewManager
    {

        private IReviewDB ReviewDB { get;  }
        public ReviewManager(IReviewDB ReviewDB)
        {
            this.ReviewDB = ReviewDB; 
        }

        public List<Review> GetAllReview()
        {
            return ReviewDB.GetAllReview(); 
        }

        public List<Review> GetAllReviewByRestaurantID(int IdRestaurant)
        {
            return ReviewDB.GetAllReviewByRestaurantID(IdRestaurant); 
        }

        public List<string> GetAllCommentByRestaurantID(int IdRestaurant)
        {
            return ReviewDB.GetAllCommentByRestaurantID(IdRestaurant); 
        }
        public void AddAReview(Review myReview)
        {
            ReviewDB.AddAReview(myReview); 
        }
    }
}
