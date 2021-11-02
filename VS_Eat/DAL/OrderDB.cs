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
                            Order MyOrder = new Order();

                            MyOrder.IdOrder = (int) dataReader["IdOrder"];
                            //MyOrder.OrderDate = (string) dataReader["OrderDate"];
                            MyOrder.DeliveryAddress = (string) dataReader["DeliveryAddress"];
                            //MyOrder.Freight = (float) dataReader["Freight"];
                            //MyOrder.TotalPrice = (float) dataReader["TotalPrice"];
                            MyOrder.IdOrderStatus = (int) dataReader["IdOrderStatus"];
                            MyOrder.IdUser = (int) dataReader["IdUser"];
                            MyOrder.IdDeliveryStaff = (int) dataReader["IdDeliveryStaff"];
                            MyOrder.IdLocation = (int) dataReader["IdLocation"];

                            // Add the restaurant to the list
                            allOrders.Add(MyOrder);

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
                            Order MyOrder = new Order();

                            MyOrder.IdOrder = (int)dataReader["IdOrder"];
                            MyOrder.OrderDate = (string)dataReader["OrderDate"];
                            MyOrder.DeliveryAddress = (string)dataReader["DeliveryAddress"];
                            MyOrder.Freight = (float)dataReader["Freight"];
                            MyOrder.TotalPrice = (float) dataReader["TotalPrice"];

                            // Add the order to the list
                            AllOrder.Add(MyOrder); 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            return AllOrder; 
        }

        //TODO [BENJI] methode pour afficher toute les openorder d'une region qui ont un statut ready



        //TODO[HUGO]: add a methode to be able to add an order in the database with all foreign key 
    }



}
