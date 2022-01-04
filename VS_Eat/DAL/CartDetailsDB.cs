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
    public class CartDetailsDB : ICartDetailsDB
    {
        private IConfiguration Configuration { get; }

        public CartDetailsDB(IConfiguration configuration)
        {
            Configuration = configuration; 
        }

        public List<CartDetails> GetAllCartDetailsFromLogin(int IdLogin)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<CartDetails> allCartDetails = new List<CartDetails>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[CartDetails] WHERE Idlogin=@IdLogin ";
                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@IdLogin", IdLogin); 
                    cn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            CartDetails cartDetails = new CartDetails();

                            cartDetails.IdCartDetails = (int)dataReader["IdCartDetails"]; 
                            cartDetails.IdLogin = (int)dataReader["IdLogin"];
                            cartDetails.IdProduct = (int)dataReader["IdProduct"];
                            cartDetails.IdRestaurant = (int)dataReader["IdRestaurant"];
                            cartDetails.ProductName = (string)dataReader["ProductName"];
                            cartDetails.ProductImage = (string)dataReader["ProductImage"]; 
                            cartDetails.Quantity = (int)dataReader["Quantity"];
                            cartDetails.UnitPrice = (float) (double) dataReader["UnitPrice"]; 

                            // Add the user to the list
                            allCartDetails.Add(cartDetails);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all cart details\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allCartDetails;
        }


        public void AddNewCartDetails(CartDetails myCartDetails)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[CartDetails](IdLogin,IdProduct,IdRestaurant,ProductName,ProductImage,Quantity,UnitPrice) values(@IdLogin,@IdProduct,@IdRestaurant,@ProductName,@ProductImage,@Quantity,@UnitPrice);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdLogin", myCartDetails.IdLogin);
                    command.Parameters.AddWithValue("@IdProduct", myCartDetails.IdProduct);
                    command.Parameters.AddWithValue("@IdRestaurant", myCartDetails.IdRestaurant);
                    command.Parameters.AddWithValue("@ProductName", myCartDetails.ProductName);
                    command.Parameters.AddWithValue("@ProductImage", myCartDetails.ProductImage); 
                    command.Parameters.AddWithValue("@Quantity", myCartDetails.Quantity);
                    command.Parameters.AddWithValue("@UnitPrice", (float) myCartDetails.UnitPrice);

                    connection.Open();

                    command.ExecuteReader(); 

                }
            }
            catch (Exception e)
            {
                Console.Write("Error while adding new cart details\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
           
        }

        public void DeleteOneEntry(int IdCartDetails)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM [dbo].[CartDetails] WHERE IdCartDetails=@IdCartDetails";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdCartDetails", IdCartDetails);
                
                    connection.Open();

                    command.ExecuteReader();

                }
            }
            catch (Exception e)
            {
                Console.Write("Error while deleting on cart details\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

        }


        /// <summary>
        /// Methode to remove all element in the chart for a specific user 
        /// </summary>
        /// <param name="IdLogin">We used the IdLogin parameter to find the corresponding user and remove all of his stuff in the chart details table</param>
        public void DeleteAllEntryByLogin(int IdLogin)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM [dbo].[CartDetails] WHERE IdLogin=@IdLogin";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdLogin", IdLogin);

                    connection.Open();

                    command.ExecuteReader();

                }
            }
            catch (Exception e)
            {
                Console.Write("Error while deleting all cart details by login\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

        }

        public void UpdateQuantity(CartDetails myCartDetail)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update [dbo].[CartDetails] Set Quantity=@Quantity WHERE IdCartDetails=@IdCartDetails;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Quantity", myCartDetail.Quantity);
                    command.Parameters.AddWithValue("@IdCartDetails", myCartDetail.IdCartDetails);


                    connection.Open();

                    command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN ADD NEW USER\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
        }
    }
}
