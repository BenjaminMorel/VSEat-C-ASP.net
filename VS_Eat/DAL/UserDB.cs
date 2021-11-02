﻿using DAL.Interfaces;
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

        public void CreateNewUser(string FirstName, string LastName, string PhoneNumber, string Email, string Password,
            string Address, int PostCode, string City)
        {
            //création de la string de connection qui va être utiliser dans la signature de addUser et addLogin
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            // We call the method EmailVerification to check if the email is already taken, if it return true we stop the method here but if the result is false we can continue
            var LoginDB = new LoginDB(Configuration);
            if (LoginDB.EmailVerification(Email))
            {
                Console.WriteLine("ERROR THIS EMAIL IS ALREADY TAKEN\nDO YOU WANT TO CONNECT ?");
                return;
            }

            //We call the methode GetLocationId to find the Id location 
            var LocationDB = new LocationDB(Configuration);
            int IdLocation = LocationDB.GetLocationId(PostCode, City);

            var NewLogin = new Login();
            NewLogin.Username = Email;
            //Faire appel à la methode de hash 
            NewLogin.Password = Password;
            NewLogin.IdLoginType = 4;

        
            //appelle de la méthode AddNewLogin pour ajouter une entrée login dans la base de donnée, la méthode prend un objet login et la string de connection créée plus haut
            int IdLogin = LoginDB.AddNewLogin(NewLogin,connectionString);
       
            if (IdLogin == -1)
            {
                Console.WriteLine("ERROR DURING THE CREATION OF THE NEW LOGIN");
                return;
            }

            //création d'une variable user qu'on va ensuite envoyé dans la signiature de la méthode addUser()
            var myUser = new User();
            myUser.IdLogin = IdLogin;
            myUser.IdLocation = IdLocation;
            myUser.FirstName = FirstName;
            myUser.LastName = LastName;
            myUser.PhoneNumber = PhoneNumber;
            myUser.Address = Address;

            //création du nouveau user dans la base de donnée via la méthode addUser()
            AddUser(myUser,connectionString);

        }
        
        //Méthode qui va venir ajouter le nouveau user dans la base de donnée via la query INSERT 
        //la méthode est private car elle sera seulement appéler via la methode createNewUser() qui elle est publique 
        private void AddUser(User myUser,string connectionString)
        {
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

            return; 
        }

        //TODO[HUGO] resortir les id des products et restaurant favoris via boucle for et parseInt 

        //TODO [HUGO] ajouter un restaurant ou un produit à la liste des favoris 

      
    }
}
