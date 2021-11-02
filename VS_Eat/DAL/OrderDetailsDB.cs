using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    class OrderDetailsDB
    {
        private IConfiguration Configuration { get; }
        public OrderDetailsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<OrderDetails> GetOrderDetails(int IdOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<OrderDetails> myOrderDetails = new List<OrderDetails>();

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
                            OrderDetails orderDetails = new OrderDetails();

                            //float ?
                            orderDetails.IdOrder = (int) dataReader["UnitPrice"];
                            orderDetails.Quantity = (int) dataReader["Quantity"];
                            orderDetails.IdProduct = (int) dataReader["IdProduct"];
                            orderDetails.IdOrder = (int) dataReader["IdOrder"];

                            //Add the orderDetails to the list
                            myOrderDetails.Add(orderDetails);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all orders\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return myOrderDetails;
        }



    }

}
