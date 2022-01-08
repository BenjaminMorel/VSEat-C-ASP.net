using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILoginManager
    {
        List<Login> GetAllLogin();

        Login GetLoginWithCredential(string Email, string Password);

        Login GetLoginByID(int IdLogin);

        Login GetLoginByUsername(string Username); 
        bool EmailVerification(string Email);

        Login UpdateLogin(Login Mylogin); 
    }
}
