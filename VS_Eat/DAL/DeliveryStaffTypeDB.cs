using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public class DeliveryStaffTypeDB : IDeliveryStaffTypeDB
    {
        private IConfiguration Configuration { get; }

        public DeliveryStaffTypeDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<DeliveryStaffType> GetAllDeliveryStaffTypes()
        {
            List<DeliveryStaffType> allDeliveryStaffTypes = new List<DeliveryStaffType>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[DeliveryStaffType]";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            DeliveryStaffType myDeliveryStaffType = new DeliveryStaffType();
                            myDeliveryStaffType.IdDeliveryStaffType = (int) dataReader["IdDeliveryStaffType"];
                            myDeliveryStaffType.DeliveryStaffTypeStr = (string) dataReader["DeliveryStaffType"];

                            allDeliveryStaffTypes.Add(myDeliveryStaffType);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all DeliveryStaffTypes\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }

            return allDeliveryStaffTypes;
        }


    }
}
