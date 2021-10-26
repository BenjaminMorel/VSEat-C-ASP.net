using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    class LocationDB 
    {
        private IConfiguration Configuration { get; }
        public LocationDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //TODO[HUGO]:  try to use the methode getLocationById
        // Methode to find the IDLocation by giving it a PostCode and a city name and the methode return a simple int that correspond to the ID 
        public int GetLocationId(int PostCode, string City)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Location] WHERE PostCode=@PostCode AND City=@City";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@PostCode", PostCode);
                    command.Parameters.AddWithValue("@City", City);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader != null)
                        {
                            return (int) dataReader["IdLocation"]; 
                        }

                        return -1; 
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN GetLocationById\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            return -1; 

        }


    }
}
