using System;
using System.Collections.Generic;
using DTO;

namespace BLL.Interfaces
{
    public interface IDeliveryStaffManager
    {
        List<DeliveryStaff> GetAllDeliveryStaff(); 

        List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff);

        DeliveryStaff CreateNewStaff(string FirstName, string LastName, string PhoneNumber, int PostCode, string City,
            string Email,
            string Password);
    }
}
