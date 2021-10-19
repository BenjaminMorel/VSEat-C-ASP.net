using DAL.Interfaces;
using DAL; 
using DTO;
using Microsoft.Extensions.Configuration;


namespace DAL
{
    public class DeliveryStaffDB : IDeliveryStaffDB
    {
        private IConfiguration Configuration { get; }
        public DeliveryStaffDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
