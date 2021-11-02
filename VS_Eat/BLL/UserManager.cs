using System.Collections.Generic;
using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Drawing;

namespace BLL
{
    public class UserManager
    {
        private IUserDB UserDb { get; }
        private ILoginDB LoginDB { get; }

        public UserManager(IConfiguration configuration)
        {
            UserDb = new UserDB(configuration);
            LoginDB = new LoginDB(configuration); 
        }

        public List<User> GetAllUsers()
        {
            return UserDb.GetAllUsers();
        }
        public User GetUserByID(string Email, string Password)
        {
            Login myLogin = LoginDB.GetLogin(Email, Password);
            return UserDb.GetUserByID(myLogin.IdLogin); 
        }


        public void CreateNewUser(string FirstName, string LastName, string PhoneNumber, string Address, string Email, string Password,
             int PostCode, string City)
        {
    
        }
    }
}
