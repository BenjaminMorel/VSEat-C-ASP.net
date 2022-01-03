using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RestaurantTypeDB : IRestaurantTypeDB
    {

        private IConfiguration Configuration { get; }

        public RestaurantTypeDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<RestaurantType> GetAllRestaurantType()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<RestaurantType> allRestaurantType = new List<RestaurantType>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[RestaurantType]";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            RestaurantType myRestaurantType = new RestaurantType();

                            myRestaurantType.IdRestaurantType = (int)dataReader["IdRestaurantType"];
                            myRestaurantType.NomRestaurantType = (string)dataReader["NomRestaurantType"]; 

                            // Add the order status to the list
                            allRestaurantType.Add(myRestaurantType);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all regions\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allRestaurantType;
        }
    }
}
