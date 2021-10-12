using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class DeliveryStaffDB
    {
        private IConfiguration Configuration { get; }
        public DeliveryStaffDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
