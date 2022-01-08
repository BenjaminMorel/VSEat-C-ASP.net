using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICartDetailsDB
    {
        List<CartDetails> GetAllCartDetailsFromLogin(int Idlogin);

        CartDetails AddNewCartDetails(CartDetails myCartDetails);

        void DeleteOneEntry(int IdProduct);

        void DeleteAllEntryByLogin(int Idlogin);
        void UpdateQuantity(CartDetails myCartDetail);
    }
}
