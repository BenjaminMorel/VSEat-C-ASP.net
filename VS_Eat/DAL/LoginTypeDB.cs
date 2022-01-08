using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using DTO;
using System.Data.SqlClient;

namespace DAL
{
    public class LoginTypeDB : ILoginTypeDB
    {
        private IConfiguration Configuration { get; }
        public LoginTypeDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method which returns a list of all login types of the database
        /// </summary>
        /// <returns> A list of all the login types</returns>
        public List<LoginType> GetAllLoginTypes()
        {
            List<LoginType> AllLoginTypes = new List<LoginType>();
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * " +
                                   "FROM [dbo].[LoginType] ";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            LoginType MyLoginType = new LoginType();

                            MyLoginType.IdLoginType = (int) dataReader["IdLoginType"];
                            MyLoginType.LoginTypeStr = (string)dataReader["LoginType"]; 

                            AllLoginTypes.Add(MyLoginType);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("Error while getting all products\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
            }
            return AllLoginTypes;
        }
    }
}
