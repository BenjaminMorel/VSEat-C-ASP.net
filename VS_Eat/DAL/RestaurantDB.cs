using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class RestaurantDB : IRestaurantDB
    {
        private IConfiguration Configuration { get; }

        public RestaurantDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method which returns a list of all the restaurants of the database
        /// </summary>
        /// <returns> Returns a list of restaurant</returns>
        public List<Restaurant> GetAllRestaurants()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Restaurant> allRestaurants = new List<Restaurant>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Restaurant]";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Restaurant myRestaurant = new Restaurant();

                            myRestaurant.IdRestaurant = (int) dataReader["IdRestaurant"];
                            myRestaurant.RestaurantName = (string) dataReader["RestaurantName"];
                            myRestaurant.RestaurantAddress = (string) dataReader["RestaurantAddress"];
                            myRestaurant.IdLogin = (int) dataReader["IdLogin"];
                            myRestaurant.IdLocation = (int) dataReader["IdLocation"];
                     
                            myRestaurant.Picture = (string)dataReader["Picture"]; 

                            // Add the restaurant to the list
                            allRestaurants.Add(myRestaurant);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all restaurants\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allRestaurants;
        }

        public Restaurant GetRestaurantByID(int IdRestaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Restaurant myRestaurant = new Restaurant();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Restaurant] WHERE IdRestaurant=@IdRestaurant";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@IdRestaurant", IdRestaurant); 

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                          
                            myRestaurant.IdRestaurant = (int)dataReader["IdRestaurant"];
                            myRestaurant.RestaurantName = (string)dataReader["RestaurantName"];
                            myRestaurant.RestaurantAddress = (string)dataReader["RestaurantAddress"];
                            myRestaurant.IdLogin = (int)dataReader["IdLogin"];
                            myRestaurant.IdLocation = (int)dataReader["IdLocation"];
                            myRestaurant.Picture = (string)dataReader["Picture"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all restaurants\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return myRestaurant;
        }

        public Restaurant GetRestaurantByIDLogin(int IdLogin)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Restaurant myRestaurant = new Restaurant();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Restaurant] WHERE IdLogin=@IdLogin";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    connection.Open();
                    cmd.Parameters.AddWithValue("@IdLogin", IdLogin);

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {

                            myRestaurant.IdRestaurant = (int)dataReader["IdRestaurant"];
                            myRestaurant.RestaurantName = (string)dataReader["RestaurantName"];
                            myRestaurant.RestaurantAddress = (string)dataReader["RestaurantAddress"];
                            myRestaurant.IdLogin = (int)dataReader["IdLogin"];
                            myRestaurant.IdLocation = (int)dataReader["IdLocation"];
                            myRestaurant.Picture = (string)dataReader["Picture"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all restaurants\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return myRestaurant;
        }



        /// <summary>
        /// Method which add a new restaurant in the database
        /// </summary>
        /// <param name="restaurant"></param>
        /// <returns> Returns an object restaurant which has been created</returns>
        public Restaurant AddRestaurant(Restaurant restaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[Restaurant]" +
                                   "(RestaurantName, RestaurantAddress, Picture, IdLogin, IdLocation, IdRestaurantType) " +
                                   "VALUES (@RestaurantName, @RestaurantAddress, @Picture, @IdLogin, @IdLocation, @IdRestaurantType);";
                    SqlCommand command = new SqlCommand(query, connection);
                    
                    command.Parameters.AddWithValue("@RestaurantName", restaurant.RestaurantName);
                    command.Parameters.AddWithValue("@RestaurantAddress", restaurant.RestaurantAddress);
                    command.Parameters.AddWithValue("@Picture", restaurant.Picture);
                    command.Parameters.AddWithValue("@IdLogin", restaurant.IdLogin);
                    command.Parameters.AddWithValue("@IdLocation", restaurant.IdLocation);
                    command.Parameters.AddWithValue("@IdRestaurantType", restaurant.IdRestaurantType);

                    connection.Open();

                    restaurant.IdRestaurant = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while adding a new restaurant\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return restaurant;
        }

        /// <summary>
        /// Method which update a restaurant given in parameter
        /// </summary>
        /// <param name="restaurant"> Object restaurant to update</param>
        /// <returns> Returns an object restaurant which has been updated</returns>
        public Restaurant UpdateRestaurant(Restaurant restaurant)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update [dbo].[Restaurant]" +
                                   "Set RestaurantName=@RestaurantName, RestaurantAddress=@RestaurantAddress, Picture=@Picture," +
                                   "IdLogin=@IdLogin, IdLocation=@IdLocation, IdRestaurantType=@IdRestaurantType" +
                                   "WHERE IdRestaurant=@IdRestaurant";
                    
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@RestaurantName", restaurant.RestaurantName);
                    command.Parameters.AddWithValue("@RestaurantAddress", restaurant.RestaurantAddress);
                    command.Parameters.AddWithValue("@Picture", restaurant.Picture);
                    command.Parameters.AddWithValue("@IdLogin", restaurant.IdLogin);
                    command.Parameters.AddWithValue("@IdLocation", restaurant.IdLocation);
                    command.Parameters.AddWithValue("@IdRestaurantType", restaurant.IdRestaurantType);
                    command.Parameters.AddWithValue("@IdRestaurant", restaurant.IdRestaurant);

                    connection.Open();

                    command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while updating restaurant " + restaurant + "\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return restaurant;
        }
    }
}
