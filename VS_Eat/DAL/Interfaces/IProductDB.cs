
using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProductDB
    {
        public List<Product> GetAllProductsFromRestaurant(int IdRestaurant); 

        //TODO GetAllProducts

        //TODO Add new product

        //TODO Update product

    }
}
