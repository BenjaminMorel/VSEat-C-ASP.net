using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderDB : IOrderDB
    {
        private IConfiguration Configuration { get; }

        public OrderDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method which returns the list of all the commands of the table
        /// </summary>
        public List<Order> GetAllOrders()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Order> allOrders = new List<Order>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Order]";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open(); 

                    using(SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Order myOrder = new Order();

                            myOrder.IdOrder = (int) dataReader["IdOrder"];
                            //myOrder.OrderDate = (string) dataReader["OrderDate"];
                            myOrder.DeliveryAddress = (string) dataReader["DeliveryAddress"];
                            //myOrder.Freight = (float) dataReader["Freight"];
                            //myOrder.TotalPrice = (float) dataReader["TotalPrice"];
                            myOrder.IdOrderStatus = (int) dataReader["IdOrderStatus"];
                            myOrder.IdUser = (int) dataReader["IdUser"];
                            myOrder.IdDeliveryStaff = (int) dataReader["IdDeliveryStaff"];
                            myOrder.IdLocation = (int) dataReader["IdLocation"];

                            // Add the restaurant to the list
                            allOrders.Add(myOrder);

                        }
                    }
                }
            } catch (Exception e)
            {
                Console.Write("Error while getting all orders\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }

            return allOrders;
        }

        /// <summary>
        /// Method which returns the list of commands by user identifier
        /// </summary>
        /// <param name="IdUser"> User number</param>
        /// <returns>List of commands</returns>
        public List<Order> GetOrderByUser(int IdUser)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Order> AllOrder = null; 

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Order] WHERE IdUser=@IdUser";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUser", IdUser);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Order myOrder = new Order();

                            myOrder.IdOrder = (int) dataReader["IdOrder"];
                            //myOrder.OrderDate = (string) dataReader["OrderDate"];
                            myOrder.DeliveryAddress = (string) dataReader["DeliveryAddress"];
                            //myOrder.Freight = (float) dataReader["Freight"];
                            //myOrder.TotalPrice = (float) dataReader["TotalPrice"];
                            myOrder.IdOrderStatus = (int) dataReader["IdOrderStatus"];
                            myOrder.IdUser = (int) dataReader["IdUser"];
                            myOrder.IdDeliveryStaff = (int) dataReader["IdDeliveryStaff"];
                            myOrder.IdLocation = (int) dataReader["IdLocation"];
                            
                            // Add the order to the list
                            AllOrder.Add(myOrder); 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting OrderByUser\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return AllOrder; 
        }

        /// <summary>
        /// Method which returns a list of orders that are with status 'ready' 
        /// </summary>
        /// <param name="IdRegion"></param>
        /// <returns></returns>
        public List<Order> GetOpenOrdersFromRegion(int IdRegion)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Order> AllOpenOrders = new List<Order>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * " +
                                   "FROM [dbo].[Order] O, Location L" +
                                   "WHERE IdOrderStatus = 2" +
                                   "AND IdRegion=@IdRegion" +
                                   "AND L.IdLocation = O.IdLocation";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Order myOrder = new Order();

                            myOrder.IdOrder = (int)dataReader["IdOrder"];
                            //myOrder.OrderDate = (string) dataReader["OrderDate"];
                            myOrder.DeliveryAddress = (string)dataReader["DeliveryAddress"];
                            //myOrder.Freight = (float) dataReader["Freight"];
                            //myOrder.TotalPrice = (float) dataReader["TotalPrice"];
                            myOrder.IdOrderStatus = (int)dataReader["IdOrderStatus"];
                            myOrder.IdUser = (int)dataReader["IdUser"];
                            myOrder.IdDeliveryStaff = (int)dataReader["IdDeliveryStaff"];
                            myOrder.IdLocation = (int)dataReader["IdLocation"];

                            // Add the order to the list
                            AllOpenOrders.Add(myOrder);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all open orders from region\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return AllOpenOrders;
        }


        }



}
