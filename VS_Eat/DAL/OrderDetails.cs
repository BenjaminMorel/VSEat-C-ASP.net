using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class OrderDetails
    {
        public float UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int IdProduct { get; set; }
        public int IdOrder { get; set; }

        public override string ToString()
        {
            return "\n__________________________\n" +
                "\nUnitPrice : " + UnitPrice +
                "\nQuantity : " + Quantity +
                "\nIdProduct : " + IdProduct +
                "\nIdOrder : " + IdOrder;
        }
    }
}
