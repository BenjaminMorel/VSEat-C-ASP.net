using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
    class OrderDetailsDB
    {
        private IConfiguration Configuration { get; }
        public OrderDetailsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
