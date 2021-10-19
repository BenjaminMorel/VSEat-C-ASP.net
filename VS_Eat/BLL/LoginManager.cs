using DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    class LoginManager
    {
        private LoginDB LoginDb { get;  }

        public LoginManager (IConfiguration configuration)
        {
            LoginDb = new LoginDB(configuration); 
        }
    }
}
