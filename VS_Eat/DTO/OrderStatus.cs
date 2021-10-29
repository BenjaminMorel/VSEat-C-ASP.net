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
        public int Status { get; set; }

        public override string ToString()
        {
            return "\n__________________________\n" +
                   "\nIdOrder : " + IdOrder +
                   "\nStatus : " + Status;
        }

    }

}
