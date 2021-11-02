using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Location
    {
        public int IdLocation { get; set; }

        public int PostCode { get; set; }

        public string City { get; set; }

        public int IdRegion { get; set; }

        public override string ToString()
        {
            return "__________________________" +
                   "\nIdLocation: " + IdLocation +
                   "\nPostCode: " + PostCode +
                   "\nCity: " + City +
                   "\nIdRegion: " + IdRegion +
                   "\n__________________________";
        }
    }
}
