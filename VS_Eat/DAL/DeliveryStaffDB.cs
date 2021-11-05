using System.Data.SqlClient;
using DAL.Interfaces;
using DAL; 
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class DeliveryStaffDB : IDeliveryStaffDB
    {
        private IConfiguration Configuration { get; }

        public DeliveryStaffDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<DeliveryStaff> GetAllDeliveryStaff()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<DeliveryStaff> allDeliveryStaff = new List<DeliveryStaff>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[DeliveryStaff]";
                    SqlCommand command = new SqlCommand(query, cn);
                    cn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            DeliveryStaff MyDeliveryStaff = new DeliveryStaff();

                            MyDeliveryStaff.IdLogin = (int)dataReader["IdLogin"];
                            MyDeliveryStaff.IdDeliveryStaff = (int)dataReader["IdDeliveryStaff"];
                            MyDeliveryStaff.FirstName = (string) dataReader["FirstName"];
                            MyDeliveryStaff.LastName = (string) dataReader["LastName"];
                            MyDeliveryStaff.PhoneNumber = (string) dataReader["PhoneNumber"];
                            MyDeliveryStaff.IdLocation = (int) dataReader["IdLocation"];

                            // Add the user to the list
                            allDeliveryStaff.Add(MyDeliveryStaff);
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
            return allDeliveryStaff;
        }
        public List<Order> CountOpenOrderByStaffId(int IdDeliveryStaff)
        {
            List<Order> numberOfOpenOrders = new List<Order>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //TODO[HUGO] ajouter contrainte de 30 min sur le compte des order open
                    string query = "SELECT * FROM [dbo].[Order] WHERE IdDeliveryStaff=@IdDeliveryStaff";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdDeliveryStaff", IdDeliveryStaff);
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

                            // Add the restaurant to the list
                            numberOfOpenOrders.Add(myOrder);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting CountOpenOrderByStaffId\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return numberOfOpenOrders;
        }

        public DeliveryStaff AddDeliveryStaff(DeliveryStaff MyStaff)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[DeliveryStaff](FirstName, Name, PhoneNumber, IdLogin,IdLocation) values(@FirstName, @Name, @PhoneNumber, IdLogin,@IdLocation);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", MyStaff.FirstName);
                    command.Parameters.AddWithValue("@LastName", MyStaff.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", MyStaff.PhoneNumber);
                    command.Parameters.AddWithValue("@IdLogin", MyStaff.IdLogin);
                    command.Parameters.AddWithValue("@IdLocation", MyStaff.IdLocation);

                    connection.Open();

                    MyStaff.IdDeliveryStaff = Convert.ToInt32(command.ExecuteScalar());

                }
            }
            catch (Exception e)
            {
                Console.Write("Error while adding new staff\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }

            return MyStaff;
        }


        
    }
}
