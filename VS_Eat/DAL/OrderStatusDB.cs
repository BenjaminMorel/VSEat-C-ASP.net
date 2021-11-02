using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL
{
    public class OrderStatusDB : IOrderStatusDB
    {
        private IConfiguration Configuration { get; }
        public OrderStatusDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<OrderStatus> GetAllOrderStatus()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<OrderStatus> allOrderStatus = new List<OrderStatus>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[OrderStatus]";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            OrderStatus myOrderStatus = new OrderStatus();

                            myOrderStatus.IdOrderStatus = (int) dataReader["IdOrderStatus"];
                            myOrderStatus.Status = (string) dataReader["Status"];

                            // Add the order status to the list
                            allOrderStatus.Add(myOrderStatus);
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
            return allOrderStatus;
        }
    }
}
