﻿using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using DAL.Interfaces;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL
{
    public class RegionDB : IRegionDB
    {
        private IConfiguration Configuration { get; }

        public RegionDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method which returns all the regions of the database
        /// </summary>
        /// <returns> Returns a list of regions</returns>
        public List<Region> GetAllRegions()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<Region> allRegions = new List<Region>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Region]";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Region myRegion = new Region();

                            myRegion.IdRegion = (int) dataReader["IdRegion"];
                            myRegion.RegionName = (string) dataReader["RegionName"];

                            // Add the order status to the list
                            allRegions.Add(myRegion);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all regions\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return allRegions;
        }

        public string GetRegionName(int IdRegion)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            string RegionName = null; 

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Region] WHERE IdRegion=@IdRegion";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdRegion", IdRegion);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
                            RegionName = (string)dataReader["RegionName"]; 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all regions\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return RegionName;
        }
    }
}
