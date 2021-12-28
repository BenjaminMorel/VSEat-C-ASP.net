using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;

namespace DAL
{
    public class OrderDetailsDB : IOrderDetailsDB
    {
        private IConfiguration Configuration { get; }
        public OrderDetailsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method which returns all order details for the order given in parameter
        /// </summary>
        /// <param name="IdOrder"></param>
        /// <returns> Returns a list of order details</returns>
        public List<OrderDetails> GetOrderDetailsFromOrder(int IdOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<OrderDetails> allOrderDetails = new List<OrderDetails>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[OrderDetails] WHERE IdOrder=@IdOrder";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            OrderDetails myOrderDetails = new OrderDetails();

                            myOrderDetails.IdOrder = (int) dataReader["IdOrder"];
                            myOrderDetails.IdProduct = (int) dataReader["IdProduct"];
                            myOrderDetails.UnitPrice = (float) (double) dataReader["UnitPrice"];
                            myOrderDetails.Quantity = (int) dataReader["Quantity"];

                            //Add the orderDetails to the list
                            allOrderDetails.Add(myOrderDetails);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all order details for order id " + IdOrder + "\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allOrderDetails;
        }

        /// <summary>
        /// Method which returns all the order details of the database
        /// </summary>
        /// <returns> Returns a list of order details</returns>
        public List<OrderDetails> GetAllOrderDetails()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<OrderDetails> allOrderDetails = new List<OrderDetails>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[OrderDetails]";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            OrderDetails myOrderDetails = new OrderDetails();

                            myOrderDetails.IdOrder = (int) dataReader["IdOrder"];
                            myOrderDetails.IdProduct = (int) dataReader["IdProduct"];
                            myOrderDetails.UnitPrice = (float) (double) dataReader["UnitPrice"];
                            myOrderDetails.Quantity = (int) dataReader["Quantity"];

                            // Add the order details to the list
                            allOrderDetails.Add(myOrderDetails);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all order details\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allOrderDetails;
        }
        public OrderDetails AddOrderDetails(OrderDetails MyOrderDetails)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<OrderDetails> allOrderDetails = new List<OrderDetails>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query =
                        "Insert Into [dbo].[OrderDetails](IdOrder,IdProduct,UnitPrice,Quantity) Values(@IdOrder,@IdProduct,@UnitPrice,@Quantity)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdOrder", MyOrderDetails.IdOrder);
                    command.Parameters.AddWithValue("@IdProduct", MyOrderDetails.IdProduct);
                    command.Parameters.AddWithValue("@UnitPrice", MyOrderDetails.UnitPrice);
                    command.Parameters.AddWithValue("@Quantity", MyOrderDetails.Quantity);
                    connection.Open();

                    command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all order details\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return MyOrderDetails; 
        }
    }
}
