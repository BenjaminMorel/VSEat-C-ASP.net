using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;

namespace BLL
{
    public class ProductManager : IProductManager
    {
        private IProductDB ProductDb { get;  }

        public ProductManager(IProductDB productDb)
        {
            this.ProductDb = productDb;
        }

        public List<Product> GetAllProducts()
        {
            return ProductDb.GetAllProducts(); 
        }
        public List<Product> GetAllProductsFromRestaurant(int IdRestaurant)
        {
            return ProductDb.GetAllProductsFromRestaurant(IdRestaurant); 
        }

        public Product GetProductByID(int IdProduct)
        {
            return ProductDb.GetProductByID(IdProduct); 
        }

        public Product AddNewProduct(Product MyProduct)
        {
            return ProductDb.AddNewProduct(MyProduct); 
        }

        public Product UpdateProduct(Product MyProduct)
        {
            return ProductDb.UpdateProduct(MyProduct); 
        }
    }
}
