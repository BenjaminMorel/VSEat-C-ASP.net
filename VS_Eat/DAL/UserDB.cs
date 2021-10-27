using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
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

        //TODO[HUGO] finish the methode to addNewUser call the methode getidLocation and call the methode that create a new user

        public void addNewUser(string FirstName, string LastName, string PhoneNumber, string Email, string Password,
            string Address, int PostCode, string City)
        {
            // We call the method EmailVerification to check if the email is already taken, if it return true we stop the method here but if the result is false we can continue
            var LoginDB = new LoginDB(Configuration);
            if (LoginDB.EmailVerification(Email))
            {
                Console.WriteLine("ERROR THIS EMAIL IS ALREADY TAKEN");
                return;
            }

            //We call the methode GetLocationId to find the Id location 

            var LocationDB = new LocationDB(Configuration);
            int IdLocation = LocationDB.GetLocationId(PostCode, City); 

            //créer une nouvelle entrée dans login 

            var NewLogin = new Login();
            NewLogin.Username = Email;
            //Faire appel à la methode de hash 
            NewLogin.Password = Password;
            NewLogin.IdLoginType = 4;
            int IdLogin = LoginDB.AddNewLogin(NewLogin);
            if (IdLogin == -1)
            {
                Console.WriteLine("ERROR DURING THE CREATION OF THE NEW LOGIN");
            }
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into User(FirstName,LastName,PhoneNumber,Address,IdLogin,IdLoginType) values(@FirstName,@LastName,@PhoneNumber,@Address,@IdLogin,@Location); SELECT SCOPE_IDENTITY()";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName",FirstName);
                    command.Parameters.AddWithValue("@LastName",LastName);
                    command.Parameters.AddWithValue("@PhoneNumber",PhoneNumber);
                    command.Parameters.AddWithValue("@Address",Address);
                    command.Parameters.AddWithValue("@IdLogin",IdLogin);
                    command.Parameters.AddWithValue("@IdLocation",IdLocation);

                    connection.Open(); 
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN ADD USER\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            Console.WriteLine("User enter correctly");
        }
    }
}
