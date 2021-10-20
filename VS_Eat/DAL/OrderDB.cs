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
     //TODO: update database and change the correct data (free,archived) and add the idLocation foreign key
    public class OrderDB : IOrderDB
    {
        private IConfiguration Configuration { get; }

        public OrderDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ShowOrder()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
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

                            MyOrder.IdOrder = (int)dataReader["IdOrder"]; 
                        }
                    }
                }
            }catch (Exception e)
            {
                Console.Write("ERROR\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
        }

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
                            MyOrder.OrderNumber = (string)dataReader["OrderNumber"];
                            MyOrder.OrderDate = (string)dataReader["OrderDate"];
                            MyOrder.Freight = (float)dataReader["Freight"];
                            MyOrder.DeliveryAddress = (string)dataReader["DeliveryAddress"];

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


        //TODO: add a methode to be able to add an order in the database with all foreign key 
    }

 
    
}
