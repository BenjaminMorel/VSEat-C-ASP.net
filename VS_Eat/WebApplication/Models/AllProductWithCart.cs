using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class AllProductWithCart
    {
        public List<Product> products { get; set; }

        public List<ChartDetails> myChart { get; set; }

        public int IdRestaurant { get; set; }
    }
}
