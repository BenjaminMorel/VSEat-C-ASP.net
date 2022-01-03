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
    public class ChartDetailsDB : IChartDetailsDB
    {
        private IConfiguration Configuration { get;  }

        public ChartDetailsDB(IConfiguration configuration)
        {
            Configuration = configuration; 
        }


        /// <summary>
        ///   We took the Id Login from the connected user and we use it to find all element of his chart
        /// </summary>
        /// <param name="IdLogin"></param>
        /// <returns>We return a list of ChartDetails, every chart details is a product in the chart</returns>
        public List<ChartDetails> GetAllChartDetailsFromLogin(int IdLogin)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<ChartDetails> allChartDetails = new List<ChartDetails>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[ChartDetails] WHERE Idlogin=@IdLogin ";
                    SqlCommand command = new SqlCommand(query, cn);
                    command.Parameters.AddWithValue("@IdLogin", IdLogin); 
                    cn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ChartDetails MyChartDetails = new ChartDetails();

                            MyChartDetails.IdChartDetails = (int)dataReader["IdChartDetails"]; 
                            MyChartDetails.IdLogin = (int)dataReader["IdLogin"];
                            MyChartDetails.IdProduct = (int)dataReader["IdProduct"];
                            MyChartDetails.IdRestaurant = (int)dataReader["IdRestaurant"];
                            MyChartDetails.ProductName = (string)dataReader["ProductName"];
                            MyChartDetails.ProductImage = (string)dataReader["ProductImage"]; 
                            MyChartDetails.Quantity = (int)dataReader["Quantity"];
                            MyChartDetails.UnitPrice = (float) (double) dataReader["UnitPrice"]; 

                            // Add the user to the list
                            allChartDetails.Add(MyChartDetails);
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
            return allChartDetails;
        }

        /// <summary>
        /// Methode to create a new entry in the chartdetails table
        /// </summary>
        /// <param name="myChartDetails">The chart details object is create in the controller methode and send here</param>
        public void AddNewChartDetails(ChartDetails myChartDetails)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[ChartDetails](IdLogin,IdProduct,IdRestaurant,ProductName,ProductImage,Quantity,UnitPrice) values(@IdLogin,@IdProduct,@IdRestaurant,@ProductName,@ProductImage,@Quantity,@UnitPrice);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdLogin", myChartDetails.IdLogin);
                    command.Parameters.AddWithValue("@IdProduct", myChartDetails.IdProduct);
                    command.Parameters.AddWithValue("@IdRestaurant", myChartDetails.IdRestaurant);
                    command.Parameters.AddWithValue("@ProductName", myChartDetails.ProductName);
                    command.Parameters.AddWithValue("@ProductImage", myChartDetails.ProductImage); 
                    command.Parameters.AddWithValue("@Quantity", myChartDetails.Quantity);
                    command.Parameters.AddWithValue("@UnitPrice", (float) myChartDetails.UnitPrice);

                    connection.Open();

                    command.ExecuteReader(); 

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


        /// <summary>
        /// Methode to remove on element from the list of product in the chart
        /// </summary>
        /// <param name="IdChartDetails">We used the IdChartDetails to find the corresponding line in the database and remove it</param>
        public void DeleteOneEntry(int IdChartDetails)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM [dbo].[ChartDetails] WHERE IdChartDetails=@IdChartDetails";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdChartDetails", IdChartDetails);
                
                    connection.Open();

                    command.ExecuteReader();

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
                    string query = "DELETE FROM [dbo].[ChartDetails] WHERE IdLogin=@IdLogin";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdLogin", IdLogin);

                    connection.Open();

                    command.ExecuteReader();

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
