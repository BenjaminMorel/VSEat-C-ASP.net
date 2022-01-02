
using DTO;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        List<Order> CountOrderWithTime(int IdDeliveryStaff);

        List<DeliveryStaff> GetAllDeliveryStaff(); 

        DeliveryStaff AddStaff(DeliveryStaff myDeliveryStaff);

        DeliveryStaff UpdateDeliveryStaff(DeliveryStaff myDeliveryStaff);

        DeliveryStaff GetDeliveryStaffByID(int IdLogin);

        List<DeliveryStaff> FindStaffFororder(int IdRegion);

    }
}
