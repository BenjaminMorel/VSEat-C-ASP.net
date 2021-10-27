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

        public void ShowAllUser()
        {
            UserDb.ShowAllUser();
        }
        public User GetUserByID(string Email, string Password)
        {
            return UserDb.GetUserByID(Email, Password); 
        }

        public void addNewUser(string FirstName, string LastName, string PhoneNumber, string Email,
            string Password,
            string Address, int PostCode, string City)
        {
            UserDb.addNewUser(FirstName,LastName,PhoneNumber,Email,Password,Address,PostCode,City);
        }
    }
}
