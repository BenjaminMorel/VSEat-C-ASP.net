

using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface ILoginDB
    {
        List<Login> GetAllLogins(); 

        Login GetLoginWithCredentials(string Email, string Password);

        Login GetLoginByID(int IdLogin);

        Login GetLoginByUsername(string Username); 
        Login AddNewLogin(Login myLogin); 

        Login EmailVerification(string Email);

        Login UpdateLogin(Login myLogin); 
    }
}
