using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Location
    {
        public int IdLocation { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }

        public override string ToString()
        {
            return "\n__________________________\n" +
                 "\nIdLocation : " + IdLocation +
                 "\nPostCode : " + PostCode +
                 "\nCity : " + City;
        }
    }
}
