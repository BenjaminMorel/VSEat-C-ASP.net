using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;


namespace DAL
{
    public class OrderDetailsDB : IOrderDetailsDB
    {
        private IConfiguration Configuration { get; }
        public OrderDetailsDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
