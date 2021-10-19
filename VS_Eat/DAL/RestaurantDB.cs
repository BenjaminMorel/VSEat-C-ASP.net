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
    public class RestaurantDB : IRestaurantDB
    {
        private IConfiguration Configuration { get; }
        public RestaurantDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ShowRestaurant()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Restaurant]";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();


                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Restaurant myRestaurant = new Restaurant();

                            myRestaurant.IdRestaurant = (int)dr["IdRestaurant"];
                            myRestaurant.RestaurantName = (string)dr["RestaurantName"];
                            myRestaurant.RestaurantAddress = (string)dr["RestaurantAddress"];

                            Console.Write(myRestaurant.ToString());
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
        }
    }
}
