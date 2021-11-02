

using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface ILoginDB
    {
        List<Login> GetAllLogin(); 

        Login GetLoginWithCredential(string Email, string Password);

        Login AddNewLogin(Login myLogin); 
        Login EmailVerification(string Email); 
    }
}
