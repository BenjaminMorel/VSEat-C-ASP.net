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
        /// Method which returns the list of all the restaurants of the table
        /// </summary>
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
    }
}
