using DAL;
using DAL.Interfaces; 
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class DeliveryStaffManager
    {
        private IDeliveryStaffDB DeliveryStaffDb { get; }

        public DeliveryStaffManager(IConfiguration configuration)
        {
               DeliveryStaffDb = new DeliveryStaffDB(configuration);
        }

    }
}
