using System.Security.Cryptography.X509Certificates;
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

        public int CountOpenOrderByStaffID(int IdDeliveryStaff)
        {
            return DeliveryStaffDb.CountOpenOrderByStaffID(IdDeliveryStaff); 
        }

        public void CreateNewStaff(string FirstName, string Name, int PostCode, string City, string Email,
            string Password)
        {
            DeliveryStaffDb.CreateNewStaff(FirstName,Name,PostCode,City,Email,Password);
        }

    }
}
