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

        public List<Product> GetAllProductsFromRestaurant(int IdRestaurant)
        {
            List<Product> products = new List<Product>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * " +
                                   "FROM [dbo].[Product]" +
                                   "WHERE IdRestaurant = @IdRestaurant";

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@IdRestaurant", IdRestaurant);
                    connection.Open();

                    using (SqlDataReader dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {

                            Product product = new Product();

                            product.IdProduct = (int)dataReader["IdProduct"];
                            product.ProductName = (string)dataReader["ProductName"];
                            product.Description = (string)dataReader["Description"];
                            //product.Price = (float)dataReader["Price"];
                            product.Picture = (string)dataReader["Picture"];
                            product.Disponibility = (bool)dataReader["Disponibility"];
                            product.IdRestaurant = (int)dataReader["IdRestaurant"];
                            product.IdProductType = (int)dataReader["IdProductType"];

                            products.Add(product);
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
            return products;
        }




    }
}