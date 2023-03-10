using System;
using System.Collections.Generic;
using DTO;

namespace BLL.Interfaces
{
    public interface IDeliveryStaffManager
    {
        DeliveryStaff GetDeliveryStaffByID(int IdLogin);

        DeliveryStaff GetDeliveryStaffByIDStaff(int IdDeliveryStaff);

        List<DeliveryStaff> GetAllDeliveryStaff();

        List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff);

        DeliveryStaff CreateNewStaff(string FirstName, string LastName, string PhoneNumber, string Address,
            int PostCode, string City, string RegionName, string Email,
            string Password);

        DeliveryStaff UpdateDeliveryStaff(DeliveryStaff myDeliveryStaff);

        List<DeliveryStaff> FindStaffFororder(int IdRegion);
    }

}
