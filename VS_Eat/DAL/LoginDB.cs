using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginDB
    {
        private IConfiguration Configuration { get; }
        public LoginDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ShowLogin()
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[Login]";
                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            Login MyLogin = new Login();

                            MyLogin.IdLogin = (int)dataReader["IdLogin"];
                            MyLogin.Username = (string)dataReader["Login"];
                            MyLogin.Password = (string)dataReader["Password"];

                            Console.Write(MyLogin.ToString()); 
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
        }
    }
}
