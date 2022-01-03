using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICartDetailsManager
    {
        List<CartDetails> GetAllCartDetailsFromLogin(int IdLogin);

        void AddNewCartDetails(CartDetails myCartDetails);

        void DeleteOneEntry(int IdProduct);

        void DeleteAllEntryByLogin(int IdLogin);
    }
}
