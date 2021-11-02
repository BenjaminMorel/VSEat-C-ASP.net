
using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IUserDB
    {
        List<User> GetAllUsers();

        User GetUserByID(int IdUser);

        void CreateNewUser(string FirstName, string LastName, string PhoneNumber, string Address, string Email, string Password, int PostCode, string City);
    }
}
