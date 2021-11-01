using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using DAL.Interfaces;
using DAL; 
using DTO;
using Microsoft.Extensions.Configuration;
using System;

namespace DAL
{
    public class DeliveryStaffDB : IDeliveryStaffDB
    {
        private IConfiguration Configuration { get; }

        public DeliveryStaffDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int CountOpenOrderByStaffID(int IdDeliveryStaff)
        {
            int NumberOfOrder = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //TODO[HUGO] ajouter contrainte de 30 min sur le compte des order open
                    string query = "SELECT * FROM [dbo].[Order] WHERE IdDeliveryStaff=@IdDeliveryStaff";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@IdDeliveryStaff", IdDeliveryStaff);
                    connection.Open();
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            NumberOfOrder++; 
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN COUNTORDERBYID\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            return NumberOfOrder;
        }


        public void CreateNewStaff(String FirstName, string Name, int PostCode, string City, string Email,
            string Password)
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
            var LocationDB = new LocationDB(Configuration);
            int IdLocation = LocationDB.GetLocationId(PostCode, City);

            var NewLogin = new Login();
            NewLogin.Username = Email;
            //Faire appel à la methode de hash 
            NewLogin.Password = Password;
            NewLogin.IdLoginType = 4;


            //appelle de la méthode AddNewLogin pour ajouter une entrée login dans la base de donnée, la méthode prend un objet login et la string de connection créée plus haut
            int IdLogin = LoginDB.AddNewLogin(NewLogin, connectionString);

            if (IdLogin == -1)
            {
                Console.WriteLine("ERROR DURING THE CREATION OF THE NEW LOGIN");
                return;
            }

            //TODO [HUGO ] la variable name s'appele LastName dans user faire pareil dans les deux class
            var DeliveryStaff = new DeliveryStaff();
            DeliveryStaff.IdLogin = IdLogin;
            DeliveryStaff.FirstNameDeliveryStaff = FirstName;
            DeliveryStaff.NameDeliveryStaff = Name;
            DeliveryStaff.IdLocation = IdLocation; 

            AddStaff(DeliveryStaff, connectionString);

        }

        private void AddStaff(DeliveryStaff MyStaff, string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "Insert into [dbo].[DeliveryStaff](FirstName,Name,IdLogin,IdLocation) values(@FirstName,@Name,IdLogin,@IdLocation);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", MyStaff.FirstNameDeliveryStaff);
                    command.Parameters.AddWithValue("@LastName", MyStaff.NameDeliveryStaff);
                    command.Parameters.AddWithValue("@IdLogin", MyStaff.IdLogin);
                    command.Parameters.AddWithValue("@IdLocation", MyStaff.IdLocation);

                    connection.Open();

                    command.ExecuteScalar();

                }
            }
            catch (Exception e)
            {
                Console.Write("ERROR IN ADD NEW STAFF\n");
                Console.Write(e.Message);
                Console.Write(e.StackTrace);
                Console.Write(e.Source);
                Console.Write("ERROR\n");
            }

            return;
        }



    }
}
