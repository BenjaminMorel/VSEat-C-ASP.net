using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DAL
{
    public class UserDB : IUserDB
    {
        private IConfiguration Configuration { get; }
        public UserDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        /// <summary>
        /// Method which returns a list of all users of the database
        /// </summary>
        /// <returns> Returns a list of users</returns>
        public List<User> GetAllUsers()
        {       
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            List<User> allUsers = new List<User>();

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [dbo].[User]";
                    SqlCommand command = new SqlCommand(query, cn);
                    cn.Open();

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            User MyUser = new User();

                            MyUser.IdUser = (int) dataReader["IdUser"];
                            MyUser.FirstName = (string) dataReader["FirstName"];
                            MyUser.LastName = (string) dataReader["LastName"];
                            MyUser.PhoneNumber = (string) dataReader["PhoneNumber"];
                            MyUser.Address = (string) dataReader["Address"];

                            // Add the user to the list
                            allUsers.Add(MyUser);
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
            return allUsers;
        }

        /// <summary>
        /// Method which returns a user according to an id login given in parameter
        /// </summary>
        /// <param name="IdLogin"> integer of the user we want to find</param>
        /// <returns> Returns an object User</returns>
        public User GetUserByID(int IdLogin)
        {
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
                            MyUser.Address = (string)dataReader["Address"];                 
    
                            MyUser.IdLogin = (int)dataReader["IdLogin"];
                            
                            MyUser.IdLocation = (int)dataReader["IdLocation"];
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MyUser.IdUser = 999; 
                Console.Write("ERROR IN GET USER BY ID\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n"); 
            }
            return MyUser;
        }

        /// <summary>
        /// Method which returns a user according to an id user given in parameter
        /// </summary>
        /// <param name="IdUser"> integer of the user we want to find</param>
        /// <returns> Returns an object User</returns>
        public User GetUserByIDUser(int IdUser)
        {
            User MyUser = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query = "Select * from [dbo].[User] WHERE IdUser=@IdUser";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdUser", IdUser);

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
                            MyUser.Address = (string)dataReader["Address"];

                            MyUser.IdLogin = (int)dataReader["IdLogin"];

                            MyUser.IdLocation = (int)dataReader["IdLocation"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                MyUser.IdUser = 999;
                Console.Write("ERROR IN GET USER BY ID\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyUser;
        }

        /// <summary>
        /// Method which add a new user in the database
        /// </summary>
        /// <param name="MyUser"></param>
        /// <returns> Returns an object User which has been added</returns>
        public User AddUser(User MyUser)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[User](FirstName,LastName,PhoneNumber,Address,IdLogin,IdLocation) values(@FirstName,@LastName,@PhoneNumber,@Address,@IdLogin,@IdLocation);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName",MyUser.FirstName);
                    command.Parameters.AddWithValue("@LastName",MyUser.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber",MyUser.PhoneNumber);
                    command.Parameters.AddWithValue("@Address",MyUser.Address);
                    command.Parameters.AddWithValue("@IdLogin",MyUser.IdLogin);
                    command.Parameters.AddWithValue("@IdLocation",MyUser.IdLocation);
            
                    connection.Open();

                    MyUser.IdUser = Convert.ToInt32(command.ExecuteScalar());

                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN ADD NEW USER\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyUser; 
        }

        /// <summary>
        /// Method which update a user in the database
        /// </summary>
        /// <param name="MyUser"></param>
        /// <returns> Returns an object User which has been updated</returns>
        public User UpdateUser(User MyUser)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Update [dbo].[User] Set FirstName=@FirstName, LastName=@LastName, PhoneNumber=@PhoneNumber, Address=@Address, IdLocation=@IdLocation WHERE IdLogin=@IdLogin;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", MyUser.FirstName);
                    command.Parameters.AddWithValue("@LastName", MyUser.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber", MyUser.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", MyUser.Address);
                    command.Parameters.AddWithValue("@IdLogin", MyUser.IdLogin);
                    command.Parameters.AddWithValue("@IdLocation", MyUser.IdLocation);

                    connection.Open();
  
                    command.ExecuteScalar(); 
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN ADD NEW USER\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }
            return MyUser;
        }

      
    }
}
