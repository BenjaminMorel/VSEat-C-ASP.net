using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginDB : ILoginDB
    {
        private IConfiguration Configuration { get; }
        public LoginDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Method which displays the list of all the logins of the table in the console
        /// </summary>
        public List<Login> GetAllLogins()
        {
            List<Login> AllLogin = new List<Login>(); 
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
                            MyLogin.Username = (string)dataReader["Username"];
                            MyLogin.Password = (string)dataReader["Password"];
                            MyLogin.IdLoginType = (int)dataReader["IdLoginType"];
                            
                            AllLogin.Add(MyLogin);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR DURING SHOW ALL LOGIN\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            return AllLogin; 
        }

        /// <summary>
        /// Method that allows you to find a login by passing an email and a password
        /// </summary>
        /// <param name="Email"> Email of the login we want to find </param>
        /// <param name="Password"> Password of the login we want to find </param>
        /// <returns> Returns an integer, the id of the corresponding login </returns>
        public Login GetLoginWithCredentials(string Email, string Password)
        {
            Login MyLogin = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Select * from [dbo].[Login] WHERE Username=@Email AND Password=@Password";
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
            return MyLogin; 
        }


        
        public Login AddNewLogin(Login MyLogin)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into Login(Username,Password,IdLoginType) values(@Username,@Password,@IdLoginType); SELECT SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username",MyLogin.Username);
                    command.Parameters.AddWithValue("@Password",MyLogin.Password);
                    command.Parameters.AddWithValue("@IdLoginType",MyLogin.IdLoginType);
                    connection.Open();
                    MyLogin.IdLogin = Convert.ToInt32(command.ExecuteScalar()); 
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN ADD NEW LOGIN\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyLogin; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public Login EmailVerification(string Email)
        {
            Login MyLogin = null; 
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Select * from [dbo].[Login] WHERE Username=@Email";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Email", Email); 
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
            return MyLogin; 
        }

        public Login UpdateLogin(Login MyLogin)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update [dbo].[Login] Set Username=@Username, Password=@Password WHERE IdLogin=@IdLogin";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username", MyLogin.Username);
                    command.Parameters.AddWithValue("@Password", MyLogin.Password);
                    command.Parameters.AddWithValue("@IdLogin",MyLogin.IdLogin);
                    connection.Open();

                    command.ExecuteScalar(); 
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN ADD UPDATE LOGIN\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyLogin;
        }

    }
}
