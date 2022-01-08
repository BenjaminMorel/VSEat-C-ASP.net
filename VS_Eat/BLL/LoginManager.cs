using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DTO;

namespace BLL
{
    public class LoginManager : ILoginManager
    {
        private ILoginDB LoginDb { get;  }

        public LoginManager (ILoginDB LoginDb)
        {
            this.LoginDb = LoginDb; 
        }

        public List<Login> GetAllLogin()
        {
            return LoginDb.GetAllLogins(); 
        }

        public Login GetLoginWithCredential(string Email, string Password)
        {
            return LoginDb.GetLoginWithCredentials(Email, Password); 
        }

        public Login GetLoginByID(int IdLogin)
        {
            return LoginDb.GetLoginByID(IdLogin); 
        }

        public Login GetLoginByUsername(string Username)
        {
            return LoginDb.GetLoginByUsername(Username); 
        }
        public bool EmailVerification(string Email)
        {
            Login MyLogin = LoginDb.EmailVerification(Email);
            if (MyLogin != null)
            {
                return true; 
            }

            return false; 
        }

        public Login UpdateLogin(Login MyLogin)
        {
            return LoginDb.UpdateLogin(MyLogin); 
        }
    }
}
