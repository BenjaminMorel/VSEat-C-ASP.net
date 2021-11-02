
using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProductDB
    {
        public List<Product> GetAllProductsFromRestaurant(int IdRestaurant);

        List<Product> GetAllProducts();

        //TODO Add new product

        //TODO Update product

    }
}
