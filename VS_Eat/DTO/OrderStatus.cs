using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class OrderStatus
    {
        public int IdOrder { get; set; }

        public string Status { get; set; }

        public override string ToString()
        {
            return "__________________________" +
                   "\nIdOrder: " + IdOrder +
                   "\nStatus: " + Status +
                   "\n__________________________";
        }

    }

}
