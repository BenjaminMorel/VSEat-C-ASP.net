using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDB : IUserDB
    {
        private IConfiguration Configuration { get; }
        public UserDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ShowAllUser()
        {       
             string connectionString = Configuration.GetConnectionString("DefaultConnection");


            try
            {
                
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[User]";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            User MyUser = new User();

                            MyUser.IdUser = (int)dr["IdUser"];
                            MyUser.FirstName = (string)dr["FirstName"];
                            MyUser.LastName = (string)dr["LastName"];
                            MyUser.PhoneNumber = (string)dr["PhoneNumber"];
                            MyUser.EmailAddress = (string)dr["EmailAddress"];


                            Console.Write(MyUser.ToString());
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

        public User GetUserByID(string Email, string Password)
        {

            var LoginDB = new LoginDB(Configuration);
            int IdLogin = LoginDB.GetLogin(Email, Password); 
            User MyUser = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query = "Select * from [dbo].[User] WHERE IdLogin=@IdLogin";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdLogin", IdLogin);

                    connection.Open();

                
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        if (dataReader.Read())
                        {
               
                            MyUser = new User();

                            MyUser.IdUser = (int)dataReader["IdUser"];
                            MyUser.FirstName = (string)dataReader["FirstName"];
                            MyUser.LastName = (string)dataReader["LastName"];
                            MyUser.PhoneNumber = (string)dataReader["PhoneNumber"];
                            MyUser.EmailAddress = (string)dataReader["EmailAddress"];
                           if (dataReader["IdLogin"] != null)
                            {
                                MyUser.IdLogin = (int)dataReader["IdLogin"];
                            }
                            if (dataReader["IdLocation"] != null)
                            {
                  
                                MyUser.IdLocation = (int)dataReader["IdLocation"];
                            }
                     
                     
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN GET USER BY ID\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyUser;
        }

        //TODO add methode to create a new user and isert it into the user and login table 
    }
}
