using BLL.Interfaces;
using DAL.Interfaces;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CartDetailsManager : ICartDetailsManager
    {
        private ICartDetailsDB CartDetailsDB { get; }

        public CartDetailsManager(ICartDetailsDB CartDetailsDB)
        {
            this.CartDetailsDB = CartDetailsDB; 
        }
    
        public List<CartDetails> GetAllCartDetailsFromLogin(int IdLogin)
        {
           return CartDetailsDB.GetAllCartDetailsFromLogin(IdLogin); 
        }

        public void AddNewCartDetails(CartDetails myChartDetails)
        {
            CartDetailsDB.AddNewCartDetails(myChartDetails); 
        }

        public void DeleteOneEntry(int IdProduct)
        {
            CartDetailsDB.DeleteOneEntry(IdProduct); 
        }

        public void DeleteAllEntryByLogin(int IdLogin)
        {
            CartDetailsDB.DeleteAllEntryByLogin(IdLogin); 
        }

        public void UpdateQuantity(CartDetails myCartDetail)
        {
            CartDetailsDB.UpdateQuantity(myCartDetail); 
        }
    }

}
