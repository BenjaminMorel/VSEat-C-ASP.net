using DTO;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class OrderStatusDB
    {
        private IConfiguration Configuration { get; }
        public OrderStatusDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

    }
}
