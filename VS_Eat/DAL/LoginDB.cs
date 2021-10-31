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

        public void ShowAllLogin()
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
                            MyLogin.Username = (string)dataReader["Username"];
                            MyLogin.Password = (string)dataReader["Password"];
                            MyLogin.IdLoginType = (int) dataReader["IdLoginType"]; 

                            Console.Write(MyLogin.ToString()); 
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
        }

        //TODO[BENJI] create a methode that take a string as argument, hash it and then compare it to the good password hash link with the same username

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


        
        public int AddNewLogin(Login NewLogin, String connectionString)
        {
            int IdLogin = -1; 
       
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into Login(Username,Password,IdLoginType) values(@Username,@Password,@IdLoginType); SELECT SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Username",NewLogin.Username);
                    command.Parameters.AddWithValue("@Password",NewLogin.Password);
                    command.Parameters.AddWithValue("@IdLoginType",NewLogin.IdLoginType);
                    connection.Open();
                    IdLogin = Convert.ToInt32(command.ExecuteScalar()); 
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

            return IdLogin; 
        }
        

        public bool EmailVerification(string Email)
        {
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
                        if (dataReader.HasRows)
                        {
                            return true; 
                        }
                        return false; 
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
            return true; 
        }
    }
}
