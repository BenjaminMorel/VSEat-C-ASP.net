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
    public class ProductDB : IProductDB
    {
        private IConfiguration Configuration { get; }
        public ProductDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method which returns a list of products for a specific restaurant
        /// </summary>
        /// <param name="IdRestaurant"></param>
        /// <returns></returns>
        public List<Product> GetAllProductsFromRestaurant(int IdRestaurant)
        {
            List<Product> products = new List<Product>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * " +
                                   "FROM [dbo].[Product] " +
                                   "WHERE IdRestaurant = @IdRestaurant";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdRestaurant", IdRestaurant);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Product product = new Product();

                            product.IdProduct = (int) dataReader["IdProduct"];
                            product.ProductName = (string) dataReader["ProductName"];
                            product.Description = (string) dataReader["Description"];
                            //product.Price = (float)dataReader["Price"];
                            product.Picture = (string) dataReader["Picture"];
                            product.Disponibility = (bool) dataReader["Disponibility"];
                            product.IdRestaurant = (int) dataReader["IdRestaurant"];
                            product.IdProductType = (int) dataReader["IdProductType"];

                            // Add the product to the list
                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all products for restaurant " + IdRestaurant + "\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return products;
        }
    }
}