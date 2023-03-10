using System;
using System.Collections.Generic;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class UserManager : IUserManager
    {
        private IUserDB UserDb { get; }

        private ILoginDB LoginDb { get; }

        private ILoginManager LoginManager { get; }

        private ILocationDB LocationDb { get;  }

        public UserManager(IUserDB userDb, ILoginDB loginDb, ILoginManager loginManager, ILocationDB locationDb)
        {
            this.UserDb = userDb;
            this.LoginDb = loginDb;
            this.LoginManager = loginManager;
            this.LocationDb = locationDb;
        }

        public List<User> GetAllUsers()
        {
            return UserDb.GetAllUsers();
        }
        public User GetUserByCredentials(string Email, string Password)
        {
            Login myLogin = LoginDb.GetLoginWithCredentials(Email, Password);
            return UserDb.GetUserByID(myLogin.IdLogin); 
        }

        public User GetUserByID(int IdLogin)
        {
            return UserDb.GetUserByID(IdLogin); 
        }

        public User GetUserByIDUser(int IdUser)
        {
            return UserDb.GetUserByIDUser(IdUser);
        }

        public User CreateNewUser(string FirstName, string LastName, string PhoneNumber, string Address, string Email, string Password,
             int PostCode, string City)
        {
            // We call the method EmailVerification to check if the email is already taken, if it return true we stop the method here but if the result is false we can continue

            if (LoginManager.EmailVerification(Email))
            {
                User user = null;
                return user; 
            }

            var Location = new Location();
            Location = LocationDb.GetLocation(PostCode, City); 

            var MyLogin = new Login();
            MyLogin.Password = Password;
            MyLogin.Username = Email; 
            // the IdLoginType for User will always be 4 
            MyLogin.IdLoginType = 4;

            //appelle de la méthode AddNewLogin pour ajouter une entrée login dans la base de donnée, la méthode prend un objet login et la string de connection créée plus haut
            MyLogin = LoginDb.AddNewLogin(MyLogin);

            var MyUser = new User();

            MyUser.IdLogin = MyLogin.IdLogin;
            MyUser.IdLocation = Location.IdLocation; 
            MyUser.FirstName = FirstName;
            MyUser.LastName = LastName;
            MyUser.PhoneNumber = PhoneNumber;
            MyUser.Address = Address;


            MyUser = UserDb.AddUser(MyUser);

            return MyUser; 
        }

        public User UpdateUser(User MyUser)
        {

            return UserDb.UpdateUser(MyUser); 
        }
    }
 
}
