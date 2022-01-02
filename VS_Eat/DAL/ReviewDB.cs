using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ReviewDB : IReviewDB
    {
        private IConfiguration Configuration { get;  }

        public ReviewDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Review> GetAllReview()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Review> allReview = new List<Review>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Review]";
                    SqlCommand command = new SqlCommand(query, cn);
                    cn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Review myReview = new Review();

                            myReview.IdRestaurant = (int)dataReader["IdRestaurant"];
                            myReview.Stars = (int)dataReader["Stars"];
                            if (dataReader["Comment"] != System.DBNull.Value)
                            {
                                myReview.Comment = (string)dataReader["Comment"];
                            }

                            allReview.Add(myReview); 
                            

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all users\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allReview;
        }

        public List<Review> GetAllReviewByRestaurantID(int IdRestaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Review> allReview = new List<Review>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Review] WHERE IdRestaurant=@IdRestaurant";
                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@IdRestaurant", IdRestaurant); 
                    cn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Review myReview = new Review();

                            myReview.IdRestaurant = (int)dataReader["IdRestaurant"];
                            myReview.Stars = (int)dataReader["Stars"];
                            if (dataReader["Comment"] != System.DBNull.Value)
                            {
                                myReview.Comment = (string)dataReader["Comment"];
                            }

                            allReview.Add(myReview);


                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all users\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allReview;
        }

        public List<string> GetAllCommentByRestaurantID(int IdRestaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<string> allComment = new List<string>();
            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Review] WHERE IdRestaurant=@IdRestaurant";
                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@IdRestaurant", IdRestaurant);
                    cn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            string comment = new string("");
                     
                            if (dataReader["Comment"] != System.DBNull.Value)
                            {
                                comment = (string)dataReader["Comment"];
                            }

                            allComment.Add(comment);


                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all users\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allComment;
        }


        public void AddAReview(Review myReview)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Review> allReview = new List<Review>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[Review](IdRestaurant,Stars,Comment) values(@IdRestaurant,@Stars,@Comment)";
                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@IdRestaurant", myReview.IdRestaurant);
                    command.Parameters.AddWithValue("@Stars", myReview.Stars);
                    command.Parameters.AddWithValue("@Comment", myReview.Comment); 
                    cn.Open();

                    command.ExecuteReader(); 
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all users\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
        }
    }
}
