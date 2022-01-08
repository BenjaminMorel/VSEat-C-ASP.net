using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class DeliveryStaffTypeManager : IDeliveryStaffTypeManager
    {
        private IDeliveryStaffTypeDB DeliveryStaffTypeDB { get; }

        public DeliveryStaffTypeManager(IDeliveryStaffTypeDB deliveryStaffTypeDB)
        {
            this.DeliveryStaffTypeDB = deliveryStaffTypeDB;
        }

        public List<DeliveryStaffType> GetAllDeliveryStaffTypes()
        {
            return DeliveryStaffTypeDB.GetAllDeliveryStaffTypes();
        }

        public DeliveryStaffType GetAllDeliveryStaffType(int IdDeliveryStaffType)
        {
            return DeliveryStaffTypeDB.GetAllDeliveryStaffType(IdDeliveryStaffType);
        }
    }
}
