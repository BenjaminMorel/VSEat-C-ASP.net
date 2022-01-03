﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CartDetails
    {
        public int IdCartDetails { get; set; }

        public int IdLogin { get; set; }

        public int IdProduct { get; set; }

        public int IdRestaurant { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set;  }

        public int Quantity { get; set; }

        public float UnitPrice { get; set;  }
    }
}