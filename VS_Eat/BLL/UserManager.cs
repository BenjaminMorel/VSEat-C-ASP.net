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
    }
}
