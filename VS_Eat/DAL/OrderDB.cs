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
        /// Method which returns a list of all the commands of the database
        /// </summary>
        /// <returns> A list of all orders</returns>
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
                            myOrder.OrderDate = (DateTime) dataReader["OrderDate"];
                            myOrder.DeliveryTime = (TimeSpan)dataReader["DeliveryTime"]; 
                            myOrder.DeliveryAddress = (string) dataReader["DeliveryAddress"];
                            myOrder.Freight = (float) (double) dataReader["Freight"];
                            myOrder.TotalPrice = (float) (double) dataReader["TotalPrice"];
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
        /// Method which returns a list of commands by user identifier
        /// </summary>
        /// <param name="IdUser"> User number</param>
        /// <returns>List of commands</returns>
        public List<Order> GetOrderByUser(int IdUser)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Order> AllOrder = new List<Order>(); 

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
                            myOrder.Freight = (float) (double) dataReader["Freight"];
                            myOrder.TotalPrice = (float) (double) dataReader["TotalPrice"];
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
        /// <returns> Returns a list of orders</returns>
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
                            myOrder.Freight = (float) (double) dataReader["Freight"];
                            myOrder.TotalPrice = (float) (double) dataReader["TotalPrice"];
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
        
        public Order AddNewOrder(Order MyOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[Order](DeliveryAddress,Freight,TotalPrice,IdOrderStatus,IdUser,IdDeliveryStaff,IdLocation) " +
                                   "Values(@DeliveryAddress,@Freight,@TotalPrice,@IdOrderStatus,@IdUser,null,@IdLocation)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@DeliveryAddress", MyOrder.DeliveryAddress);
                    command.Parameters.AddWithValue("@Freight", MyOrder.Freight);
                    command.Parameters.AddWithValue("@TotalPrice", MyOrder.TotalPrice);
                    command.Parameters.AddWithValue("@IdOrderStatus", MyOrder.IdOrderStatus);
                    command.Parameters.AddWithValue("@IdUser", MyOrder.IdUser);
                    command.Parameters.AddWithValue("@IdLocation",MyOrder.IdLocation);
                    connection.Open();

                    MyOrder.IdOrder = Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while creating a new Order\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return MyOrder;
        }
        public Order CancelOrder(Order MyOrder,User MyUser)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update [dbo].[Order] Set IdOrderStatus=@IdOrderStatus " +
                                   "FROM [dbo].[Order] O, [dbo].[User] U " +
                                   "WHERE IdOrder=@IdOrder " +
                                   "AND LastName=@LastName " +
                                   "AND FirstName=@FirstName " +
                                   "AND O.IdUser=U.IdUser";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdOrderStatus", MyOrder.IdOrderStatus);
                    command.Parameters.AddWithValue("@IdOrder",MyOrder.IdOrder);
                    command.Parameters.AddWithValue("@Lastname",MyUser.LastName);
                    command.Parameters.AddWithValue("@FirstName",MyUser.FirstName);
                    connection.Open();

                    command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN CANCELLED ORDER\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyOrder;
        }


        public Order UpdateOrderStatus(Order MyOrder)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update [dbo].[Order] Set IdOrderStatus=@IdOrderStatus " + 
                                   "WHERE IdOrder=@IdOrder";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdOrderStatus", MyOrder.IdOrderStatus);
                    command.Parameters.AddWithValue("@IdOrder", MyOrder.IdOrder);
                    connection.Open();

                    command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN UPDATE ORDERSTATUS\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyOrder;
        }

        public Order AssignStaffToAnOrder(Order MyOrder)
        {

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update [dbo].[Order] Set IdDeliveryStaff=@IdDeliveryStaff " +
                                   "WHERE IdOrder=@IdOrder";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdDeliveryStaff", MyOrder.IdDeliveryStaff);
                    command.Parameters.AddWithValue("@IdOrder", MyOrder.IdOrder);
                    connection.Open();

                    command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN UPDATE ORDERSTATUS\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyOrder;
        }
    }



}
