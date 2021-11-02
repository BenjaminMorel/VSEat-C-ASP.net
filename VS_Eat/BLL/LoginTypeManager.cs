using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class LoginTypeManager : ILoginTypeManager
    {

        private ILoginTypeDB LoginTypeDB { get;  }
        public LoginTypeManager(IConfiguration configuration)
        {
            LoginTypeDB = new LoginTypeDB(configuration); 
        }
    }
}
