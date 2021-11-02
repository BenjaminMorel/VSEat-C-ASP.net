using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using System.Data.SqlClient;

namespace DAL
{
    public class OrderDetailsDB : IOrderDetailsDB
    {
        private IConfiguration Configuration { get; }
        public OrderDetailsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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

                            //float ?
                            myOrderDetails.IdOrder = (int) dataReader["UnitPrice"];
                            myOrderDetails.Quantity = (int) dataReader["Quantity"];
                            myOrderDetails.IdProduct = (int) dataReader["IdProduct"];
                            myOrderDetails.IdOrder = (int) dataReader["IdOrder"];

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

                            //float ?
                            myOrderDetails.IdOrder = (int) dataReader["UnitPrice"];
                            myOrderDetails.Quantity = (int) dataReader["Quantity"];
                            myOrderDetails.IdProduct = (int) dataReader["IdProduct"];
                            myOrderDetails.IdOrder = (int) dataReader["IdOrder"];

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
    }
}
