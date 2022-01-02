using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IReviewDB
    {
        List<Review> GetAllReview();
        List<Review> GetAllReviewByRestaurantID(int IdRestaurant);

        List<string> GetAllCommentByRestaurantID(int IdRestaurant);

        void AddAReview(Review myReview);
    }
}
