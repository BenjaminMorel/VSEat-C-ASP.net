using System;
using System.Collections.Generic;
using DTO;

namespace BLL.Interfaces
{
    public interface IDeliveryStaffManager
    {
        List<DeliveryStaff> GetAllDeliveryStaff(); 

        List<Order> CountOrderWithTime(int IdDeliveryStaff, DateTime timeControl);

        DeliveryStaff CreateNewStaff(string FirstName, string LastName, string PhoneNumber, string Address,
            int PostCode, string City, string RegionName, string Email,
            string Password);

        DeliveryStaff UpdateDeliveryStaff(DeliveryStaff myDeliveryStaff);

        DeliveryStaff GetDeliveryStaffByID(int IdLogin);

        List<DeliveryStaff> FindStaffFororder(int IdRegion);
    }

}
