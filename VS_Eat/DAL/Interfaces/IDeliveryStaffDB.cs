
using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        List<Order> CountOpenOrderByStaffId(int IdDeliveryStaff);

        List<DeliveryStaff> GetAllDeliveryStaff(); 

        DeliveryStaff AddStaff(DeliveryStaff myDeliveryStaff);

        DeliveryStaff GetDeliveryStaffByID(int IdLogin);

    }
}
