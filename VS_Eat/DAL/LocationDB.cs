using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        /// <summary>
        /// Method which returns a list of all the locations of the database
        /// </summary>
        /// <returns> list of all locations</returns>
        public List<Location> GetAllLocations()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Location> AllLocations = new List<Location>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Location]";
                    SqlCommand command = new SqlCommand(query, cn);
                    cn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Location MyLocation = new Location();

                            MyLocation.IdLocation = (int)dataReader["IdLocation"]; 
                            MyLocation.PostCode = (int)dataReader["PostCode"]; 
                            MyLocation.City = (string)dataReader["City"];
                            MyLocation.IdRegion = (int)dataReader["IdRegion"]; 
                            
                            AllLocations.Add(MyLocation);
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
            return AllLocations;
        }

        /// <summary>
        /// Method used find the IDLocation by giving it a PostCode and a city name
        /// </summary>
        /// <param name="PostCode"></param>
        /// <param name="City"></param>
        /// <returns> Simple int that correspond to the ID </returns>
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
