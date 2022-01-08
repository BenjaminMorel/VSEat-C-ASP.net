
using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface IUserDB
    {
        List<User> GetAllUsers();

        User GetUserByID(int IdUser);

        User GetUserByIDUser(int IdUser);

        User AddUser(User MyUser);

        User UpdateUser(User MyUser);


    }
}

