using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using DAL.Interfaces;
using DAL; 
using DTO;
using Microsoft.Extensions.Configuration;
using System;

namespace DAL
{
    public class DeliveryStaffDB : IDeliveryStaffDB
    {
        private IConfiguration Configuration { get; }

        public DeliveryStaffDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int CountOpenOrderByStaffID(int IdDeliveryStaff)
        {
            int NumberOfOrder = 0;
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
                            NumberOfOrder++; 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN COUNTORDERBYID\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            return NumberOfOrder;
        }



    }
}
