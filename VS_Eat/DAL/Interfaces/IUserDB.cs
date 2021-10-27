
using DTO;

namespace DAL.Interfaces
{
    public interface IUserDB
    {
        void ShowAllUser();

        User GetUserByID(string Email, string Password);

        void addNewUser(string FirstName, string LastName, string PhoneNumber, string Email, string Password,
            string Address, int PostCode, string City);
    }
}
