using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL.Interfaces
{
    interface IUserManager
    {
        List<User> GetAllUsers();

        User GetUserByID(string Email, string Password);

        User CreateNewUser(string FirstName, string LastName, string PhoneNumber, string Address, string Email,
            string Password,
            int PostCode, string City);

        User UpdateUser(User MyUser, string City, int PostCode); 
    }
}
