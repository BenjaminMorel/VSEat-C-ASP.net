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

        public void ShowALLLogin()
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


        public int GetLogin(string Email, string Password)
        {
            Login MyLogin = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
              
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Select * from Login WHERE Username=@Email AND Password=@Password";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Password", Password);
                    connection.Open();
      
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {

                  
                           MyLogin = new Login();

                            MyLogin.IdLogin = (int)dataReader["IdLogin"];
                            MyLogin.Username = (string)dataReader["Username"];
                            MyLogin.Password = (string)dataReader["Password"];
                            MyLogin.IdLoginType = (int)dataReader["IdLoginType"];

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
            if(MyLogin  != null)
            {
                return MyLogin.IdLogin;
            }
            return 0; 
           
        }
    }
}
