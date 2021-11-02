using System.Collections.Generic;
using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;


namespace BLL
{
    public class UserManager
    {
        private IUserDB UserDb { get; }

        public UserManager(IConfiguration configuration)
        {
            UserDb = new UserDB(configuration); 
        }

        public List<User> GetAllUsers()
        {
            return UserDb.GetAllUsers();
        }
        public User GetUserByID(string Email, string Password)
        {
            return UserDb.GetUserByID(Email, Password); 
        }

        public void CreateNewUser(string FirstName, string LastName, string PhoneNumber, string Address, string Email, string Password,
             int PostCode, string City)
        { UserDb.CreateNewUser(FirstName,LastName,PhoneNumber, Address, Email,Password,PostCode,City);
        }
    }
}
