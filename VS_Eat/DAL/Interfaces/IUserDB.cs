
using DTO;

namespace DAL.Interfaces
{
    public interface IUserDB
    {
        void ShowAllUser();

        User GetUserByID(int IdUser);

        void CreateNewUser(string FirstName, string LastName, string PhoneNumber, string Address, string Email, string Password, int PostCode, string City);
    }
}
