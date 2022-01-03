using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL.Interfaces
{
    public interface IDeliveryStaffTypeDB
    {
        List<DeliveryStaffType> GetAllDeliveryStaffTypes();

        DeliveryStaffType GetAllDeliveryStaffType(int IdDeliveryStaffType);
    }
}
