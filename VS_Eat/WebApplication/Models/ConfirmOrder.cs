using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ConfirmOrder
    {
        public List<CartDetails> allCartDetails { get; set;  }

        public double freight { get; set;  }

        public double TotalPrice { get; set;  }

        public int errorCode { get; set;  }
        public ConfirmOrder(List<CartDetails> allCartDetails, double freight)
        {
            this.allCartDetails = allCartDetails;
            this.freight = freight;

            foreach(var product in allCartDetails)
            {
                TotalPrice += product.Quantity * product.UnitPrice; 
            }
            TotalPrice += freight; 
        }
    }
}
