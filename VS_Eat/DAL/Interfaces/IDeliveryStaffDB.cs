
using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff);

        void CreateNewStaff(string FirstName, string Name, int PostCode, string City, string Email,
            string Password);

    }
}
