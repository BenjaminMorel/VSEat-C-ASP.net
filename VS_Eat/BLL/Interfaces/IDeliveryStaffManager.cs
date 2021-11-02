using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL.Interfaces
{
    public interface IDeliveryStaffManager
    {
        List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff);

        DeliveryStaff CreateNewStaff(string FirstName, string LastName, string PhoneNumber, int PostCode, string City,
            string Email,
            string Password);
    }
}
