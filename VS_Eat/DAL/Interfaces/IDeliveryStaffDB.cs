
using DTO;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff);
        List<DeliveryStaff> GetAllDeliveryStaff(); 
        List<DeliveryStaff> GetAllDeliveryStaff(); 

        DeliveryStaff GetDeliveryStaffByID(int IdLogin);

        List<DeliveryStaff> GetAllDeliveryStaffByType(int IdDeliveryStaffType);

        List<Order> CountOrderWithTime(int IdDeliveryStaff);

        DeliveryStaff AddStaff(DeliveryStaff myDeliveryStaff);

        DeliveryStaff UpdateDeliveryStaff(DeliveryStaff myDeliveryStaff);

       List<DeliveryStaff> FindStaffFororder(int IdRegion);

    }
}
