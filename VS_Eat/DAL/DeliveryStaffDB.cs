using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DeliveryStaffDB
    {
        private IConfiguration Configuration { get; }
        public DeliveryStaffDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
