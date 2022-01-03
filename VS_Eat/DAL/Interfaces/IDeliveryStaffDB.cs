
using DTO;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff);

        List<DeliveryStaff> GetAllDeliveryStaff();

        DeliveryStaff GetDeliveryStaffByID(int IdLogin);
        
        DeliveryStaff AddStaff(DeliveryStaff myDeliveryStaff);

        DeliveryStaff UpdateDeliveryStaff(DeliveryStaff myDeliveryStaff);

       List<DeliveryStaff> FindStaffFororder(int IdRegion);

    }
}
