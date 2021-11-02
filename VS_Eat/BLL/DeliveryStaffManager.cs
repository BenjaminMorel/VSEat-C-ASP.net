using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class DeliveryStaffManager : IDeliveryStaffManager
    {
        private IDeliveryStaffDB DeliveryStaffDb { get; }

        public DeliveryStaffManager(IConfiguration configuration)
        {
            DeliveryStaffDb = new DeliveryStaffDB(configuration);
        }

        public List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff)
        {
            return DeliveryStaffDb.CountOpenOrderByStaffID(IdDeliveryStaff); 
        }

        //TODO [Modifier le retour et la structur de la methode DAL/BLL
        public void CreateNewStaff(string FirstName, string Name, int PostCode, string City, string Email,
            string Password)
        {
            DeliveryStaffDb.CreateNewStaff(FirstName,Name,PostCode,City,Email,Password);
        }

    }
}
