
using DTO;

namespace DAL.Interfaces
{
    public interface IUserDB
    {
        void ShowAllUser();

        User GetUserByID(string Email, string Password); 
    }
}
