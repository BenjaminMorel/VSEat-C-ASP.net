using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL.Interfaces
{
    public interface IUserManager
    {
        List<User> GetAllUsers();

        User GetUserByCredentials(string Email, string Password);

        User GetUserByID(int IdLogin);

        User GetUserByIDUser(int IdUser);

        User CreateNewUser(string FirstName, string LastName, string PhoneNumber, string Address, string Email,
            string Password,
            int PostCode, string City);

        User UpdateUser(User MyUser); 
    }
}
