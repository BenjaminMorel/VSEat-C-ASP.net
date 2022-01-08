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

        public OrderDetails()
        {

        }
        public OrderDetails(float unitPrice, int quantity, int idProduct, int idOrder)
        {
            UnitPrice = unitPrice;
            Quantity = quantity;
            IdProduct = idProduct;
            IdOrder = idOrder;
        }

    }
}
