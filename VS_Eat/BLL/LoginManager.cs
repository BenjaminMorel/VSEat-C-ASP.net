using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoginManager
    {
        private ILoginDB LoginDb { get;  }

        public LoginManager (IConfiguration configuration)
        {
            LoginDb = new LoginDB(configuration); 
        }

        public void ShowAllLogin()
        {
            LoginDb.ShowAllLogin();
        }

        public int GetLogin(string Email, string Password)
        {
            return GetLogin(Email, Password); 
        }

        public bool EmailVerification(string Email)
        {
            return LoginDb.EmailVerification(Email); 
        }
    }
}
