
using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        List<Order> CountOpenOrderByStaffId(int IdDeliveryStaff);

        //void CreateNewStaff(string FirstName, string Name, int PostCode, string City, string Email, string Password);

        //DeliveryStaff AddDeliveryStaff(DeliveryStaff myDeliveryStaff);

    }
}
