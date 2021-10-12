using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class LoginType
    {
        public int IdLoginType { get; set; }
        public string LoginTypeStr { get; set; }
        public override string ToString()
        {
            return "\n__________________________\n" +
                "\nIdLoginType : " + IdLoginType +
                "\nLoginType : " + LoginTypeStr;
        }
    }
}
