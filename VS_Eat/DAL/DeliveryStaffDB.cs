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
        /// <summary>
        /// Method which returns a list of all the delivery staff of the database
        /// </summary>
        /// <returns>list of all the delivery staff</returns>
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
                            MyDeliveryStaff.IdDeliveryStaffType = (int) dataReader["IdDeliveryStaffType"];
                            MyDeliveryStaff.IdWorkingRegion = (int) dataReader["IdWorkingRegion"];

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

        public DeliveryStaff GetDeliveryStaffByID(int IdLogin)
        {
            DeliveryStaff myDeliveryStaff = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query = "Select * from [dbo].[DeliveryStaff] WHERE IdLogin=@IdLogin";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdLogin", IdLogin);

                    connection.Open();


                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            myDeliveryStaff = new DeliveryStaff();

                            myDeliveryStaff.IdDeliveryStaff = (int)dataReader["IdDeliveryStaff"];
                            myDeliveryStaff.FirstName = (string)dataReader["FirstName"];
                            myDeliveryStaff.LastName = (string)dataReader["LastName"];
                            myDeliveryStaff.PhoneNumber = (string)dataReader["PhoneNumber"];
                            myDeliveryStaff.Address = (string)dataReader["Address"];
                            myDeliveryStaff.IdLogin = (int)dataReader["IdLogin"];
                            myDeliveryStaff.IdLocation = (int)dataReader["IdLocation"];
                            myDeliveryStaff.IdDeliveryStaffType = (int) dataReader["IdDeliveryStaffType"];
                            myDeliveryStaff.IdWorkingRegion = (int) dataReader["IdWorkingRegion"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                myDeliveryStaff.IdDeliveryStaffType = 999;
                Console.Write("ERROR IN GET USER BY ID\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            return myDeliveryStaff;
        }

        public List<DeliveryStaff> GetAllDeliveryStaffByType(int IdDeliveryStaff)
        {
            List<DeliveryStaff> listDeliveryStaff = new List<DeliveryStaff>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[DeliveryStaff] WHERE IdDeliveryStaffType=@IdDeliveryStaff";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            DeliveryStaff myDeliveryStaff = new DeliveryStaff();

                            myDeliveryStaff.IdLogin = (int)dataReader["IdLogin"];
                            myDeliveryStaff.IdDeliveryStaff = (int)dataReader["IdDeliveryStaff"];
                            myDeliveryStaff.FirstName = (string)dataReader["FirstName"];
                            myDeliveryStaff.LastName = (string)dataReader["LastName"];
                            myDeliveryStaff.PhoneNumber = (string)dataReader["PhoneNumber"];
                            myDeliveryStaff.IdLocation = (int)dataReader["IdLocation"];
                            myDeliveryStaff.IdDeliveryStaffType = (int)dataReader["IdDeliveryStaffType"];
                            myDeliveryStaff.IdWorkingRegion = (int)dataReader["IdWorkingRegion"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN GET LOGIN\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            return listDeliveryStaff;
        }

        /// <summary>
        /// Method which returns a list of open order for a deliver by id
        /// </summary>
        /// <param name="IdDeliveryStaff"></param>
        /// <returns></returns>
        public List<Order> CountOpenOrderByStaffId(int IdDeliveryStaff)
        {
            List<Order> numberOfOpenOrders = new List<Order>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //TODO[HUGO] ajouter contrainte de 30 min sur le compte des order open
                    string query = "SELECT * FROM [dbo].[Order] WHERE IdDeliveryStaff=@IdDeliveryStaff ";

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

        /// <summary>
        /// Method which add a new delivery staff in the table
        /// </summary>
        /// <param name="MyStaff">Object DeliveryStaff</param>
        /// <returns></returns>
        public DeliveryStaff AddStaff(DeliveryStaff MyStaff)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[DeliveryStaff] (FirstName, LastName, PhoneNumber, Address, IdLogin, IdLocation, IdDeliveryStaffType, IdWorkingRegion) values (@FirstName, @LastName, @PhoneNumber, @Address, @IdLogin, @IdLocation, @IdDeliveryStaffType, @IdWorkingRegion)";
                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@FirstName", MyStaff.FirstName);
                    command.Parameters.AddWithValue("@LastName", MyStaff.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", MyStaff.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", MyStaff.Address);
                    //command.Parameters.AddWithValue("@IdLogin", MyStaff.IdLogin);
                    //command.Parameters.AddWithValue("@IdLocation", MyStaff.IdLocation);
                    //command.Parameters.AddWithValue("@IdDeliveryStaffType", MyStaff.IdDeliveryStaffType);
                    //command.Parameters.AddWithValue("@IdWorkingRegion", MyStaff.IdWorkingRegion);

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
