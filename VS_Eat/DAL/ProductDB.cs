using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class ProductDB
    {
        private IConfiguration Configuration { get; }
        public ProductDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
