using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL.Interfaces
{
    public interface IProductManager
    {
        List<Product> GetAllProducts();
        List<Product> GetAllProductsFromRestaurant(int IdRestaurant);

        Product AddNewProduct(Product MyProduct);

        Product UpdateProduct(Product myProduct); 
    }
}
