
using DTO;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff);

        DeliveryStaff AddStaff(DeliveryStaff myDeliveryStaff);

        DeliveryStaff UpdateDeliveryStaff(DeliveryStaff myDeliveryStaff);

        DeliveryStaff GetDeliveryStaffByID(int IdLogin);

        List<DeliveryStaff> FindStaffFororder(int IdRegion);

    }
}
