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
        /// Method which returns the list of all the users of the table
        /// </summary>
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
                            MyUser.FavoriteRestaurant = (string) dataReader["FavoriteRestaurant"];
                            MyUser.FavoriteProduct = (string) dataReader["FavoriteProduct"];

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
                            MyUser.Address = (string)dataReader["EmailAddress"];
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

        //Méthode qui va venir ajouter le nouveau user dans la base de donnée via la query INSERT 
        //la méthode est private car elle sera seulement appéler via la methode createNewUser() qui elle est publique 
        public User AddUser(User myUser)
        {
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[User](FirstName,LastName,PhoneNumber,Address,IdLogin,IdLocation) values(@FirstName,@LastName,@PhoneNumber,@Address,@IdLogin,@IdLocation);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName",myUser.FirstName);
                    command.Parameters.AddWithValue("@LastName",myUser.LastName);
                    command.Parameters.AddWithValue("@PhoneNumber",myUser.PhoneNumber);
                    command.Parameters.AddWithValue("@Address",myUser.Address);
                    command.Parameters.AddWithValue("@IdLogin",myUser.IdLogin);
                    command.Parameters.AddWithValue("@IdLocation",myUser.IdLocation);

                    connection.Open();

                    myUser.IdLogin = Convert.ToInt32(command.ExecuteScalar());
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
            return myUser; 
        }
        
        
        //TODO [?] update les informations du user
        //TODO[HUGO] resortir les id des products et restaurant favoris via boucle for et parseInt 

        //TODO [HUGO] ajouter un restaurant ou un produit à la liste des favoris 

      
    }
}
