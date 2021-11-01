using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Login
    {
        public int IdLogin { get; set; }    

        public string Username{ get; set; }

        public string Password { get; set; }

        public int IdLoginType { get; set; }

        public override string ToString()
        {
            return "__________________________" +
                    "\nId Login : " + IdLogin +
                    "\nUsername : " + Username +
                    "\nPassword : " + Password +
                    "\nId Login Type : " + IdLoginType +
                    "\n__________________________";
        }
    }
}
