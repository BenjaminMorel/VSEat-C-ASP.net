
using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IUserDB
    {
        List<User> GetAllUsers();

        User GetUserByID(int IdUser);

        User AddUser(User myUser); 
    }
}

