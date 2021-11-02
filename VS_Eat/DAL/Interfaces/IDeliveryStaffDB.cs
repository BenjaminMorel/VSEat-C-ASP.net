
using DTO;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        List<Order> CountOpenOrderByStaffId(int IdDeliveryStaff);

    
        DeliveryStaff AddDeliveryStaff(DeliveryStaff myDeliveryStaff);

    }
}
