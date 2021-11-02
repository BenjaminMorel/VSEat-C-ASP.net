using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;


namespace DAL
{
    public class LoginTypeDB : ILoginTypeDB
    {
        private IConfiguration Configuration { get; }
        public LoginTypeDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
