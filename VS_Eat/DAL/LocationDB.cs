using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using DAL.Interfaces;


namespace DAL
{
    public class LocationDB : ILocationDB
    {
        private IConfiguration Configuration { get; }
        public LocationDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Methode to find the IDLocation by giving it a PostCode and a city name and the methode return a simple int that correspond to the ID 
        public Location GetLocation(int PostCode, string City)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            Location myLocation = new Location();

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

                        if (dataReader.Read())
                        {
                            myLocation.IdLocation = (int) dataReader["IdLocation"];
                            myLocation.PostCode = (int) dataReader["PostCode"];
                            myLocation.City = (string) dataReader["City"];
                            myLocation.IdRegion = (int) dataReader["IdRegion"];
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting LocationById\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return myLocation;
        }


    }
}
