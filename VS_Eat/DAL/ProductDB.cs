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

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * " +
                                   "FROM [dbo].[Product] ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Product product = new Product();

                            product.IdProduct = (int)dataReader["IdProduct"];
                            product.ProductName = (string)dataReader["ProductName"];
                            product.Description = (string)dataReader["Description"];
                            //TODO regler problème avec le price
                            //product.Price = (float) dataReader["Price"];
                            product.Picture = (string)dataReader["Picture"];
                            product.Disponibility = (bool)dataReader["Disponibility"];
                            product.IdRestaurant = (int)dataReader["IdRestaurant"];
                            product.IdProductType = (int)dataReader["IdProductType"];

                            // Add the product to the list
                            products.Add(product);
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
            return products;
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

        public Product AddNewProduct(Product MyProduct)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[Product](ProductName, Description, Price, Picture, Disponibility, Vegetarian, IdRestaurant, IdProductType) values(@ProductName, @Description, @Price, @Picture, @Disponibility, @Vegetarian, @IdRestaurant, @IdProductType);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductName", MyProduct.ProductName);
                    command.Parameters.AddWithValue("@Description", MyProduct.Description);
                    command.Parameters.AddWithValue("@Price", MyProduct.Price);
                    command.Parameters.AddWithValue("@Picture", MyProduct.Picture);
                    command.Parameters.AddWithValue("@Disponibility", MyProduct.Disponibility);
                    command.Parameters.AddWithValue("@Vegetarian", MyProduct.Vegetarian);
                    command.Parameters.AddWithValue("@IdRestaurant", MyProduct.IdRestaurant);
                    command.Parameters.AddWithValue("@IdProductType", MyProduct.IdProductType);
                 

                    connection.Open();

                    MyProduct.IdProduct = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN ADD NEW PRODUcT\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyProduct;
        }

        public Product UpdateProduct(Product MyProduct)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update [dbo].[Product]" +
                                   "Set ProductName=@ProductName, Description=@Description, Picture=@Picture, Disponibility=@Disponibility, Vegetarian=@Vegetarian WHERE IdProduct=@IdProduct";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ProductName", MyProduct.ProductName);
                    command.Parameters.AddWithValue("@Description", MyProduct.Description);
         //           command.Parameters.AddWithValue("@Price", MyProduct.Price);
                    command.Parameters.AddWithValue("@Picture", MyProduct.Picture);
                    command.Parameters.AddWithValue("@Disponibility", MyProduct.Disponibility);
                    command.Parameters.AddWithValue("@Vegetarian", MyProduct.Vegetarian);
                    command.Parameters.AddWithValue("@IdProduct", MyProduct.IdProduct);


                    connection.Open();

                    command.ExecuteScalar(); 
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN UPDATE PRODUCT\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyProduct;
        }



    }
}