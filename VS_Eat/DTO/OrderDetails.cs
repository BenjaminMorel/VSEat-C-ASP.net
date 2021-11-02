using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderDetails
    {
        public float UnitPrice { get; set; }

        public int Quantity { get; set; }

        public int IdProduct { get; set; }

        public int IdOrder { get; set; }

        public override string ToString()
        {
            return "__________________________" +
                   "\nUnitPrice : " + UnitPrice +
                   "\nQuantity : " + Quantity +
                   "\nIdProduct : " + IdProduct +
                   "\nIdOrder : " + IdOrder +
                   "\n__________________________";
        }
    }
}
