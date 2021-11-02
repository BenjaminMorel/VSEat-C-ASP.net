using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL
{
    public class OrderStatusDB : IOrderStatusDB
    {
        private IConfiguration Configuration { get; }
        public OrderStatusDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

    }
}
