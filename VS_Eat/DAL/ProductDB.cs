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
                            Product myProduct = new Product();

                            myProduct.IdProduct = (int) dataReader["IdProduct"];
                            myProduct.ProductName = (string) dataReader["ProductName"];
                            myProduct.Description = (string) dataReader["Description"];
                            //product.Price = (float)dataReader["Price"];
                            myProduct.Picture = (string) dataReader["Picture"];
                            myProduct.Disponibility = (bool) dataReader["Disponibility"];
                            myProduct.Vegetarian = (bool) dataReader["Vegetarian"];
                            myProduct.IdRestaurant = (int) dataReader["IdRestaurant"];
                            myProduct.IdProductType = (int) dataReader["IdProductType"];

                            // Add the product to the list
                            products.Add(myProduct);
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

        public List<Product> GetAllProducts()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Product> allProducts = new List<Product>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Product]";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Product myProduct = new Product();

                            myProduct.IdProduct = (int) dataReader["IdProduct"];
                            myProduct.ProductName = (string) dataReader["ProductName"];
                            myProduct.Description = (string) dataReader["Description"];
                            //product.Price = (float) dataReader["Price"];
                            myProduct.Picture = (string) dataReader["Picture"];
                            myProduct.Disponibility = (bool) dataReader["Disponibility"];
                            myProduct.Vegetarian = (bool) dataReader["Vegetarian"];
                            myProduct.IdRestaurant = (int) dataReader["IdRestaurant"];
                            myProduct.IdProductType = (int) dataReader["IdProductType"];

                            // Add the order status to the list
                            allProducts.Add(myProduct);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all products\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allProducts;
        }


        //TODO [?] un restaurant peu ajouter un nouveau produit ? ou mettre la methode ? 



    }
}