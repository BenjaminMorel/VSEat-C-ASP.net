using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductManager
    {
        private IProductDB ProductDb { get;  }

        public ProductManager(IConfiguration configuration)
        {
            ProductDb = new ProductDB(configuration); 
        }

        public List<Product> GetAllProductsFromRestaurant(int IdRestaurant)
        {
            return ProductDb.GetAllProductsFromRestaurant(IdRestaurant); 
        }
    }
}
