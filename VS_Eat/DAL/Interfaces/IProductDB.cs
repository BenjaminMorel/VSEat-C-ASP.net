﻿
using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IProductDB
    {
        List<Product> GetAllProductsFromRestaurant(int IdRestaurant); 

        
        List<Product> GetAllProducts(); 

        Product AddNewProduct(Product MyProduct); 


        Product UpdateProduct(Product MyProduct); 
    }
}
