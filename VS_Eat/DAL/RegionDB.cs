using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using DAL.Interfaces;
using DAL;
using DTO;
using Microsoft.Extensions.Configuration;
using System;

namespace DAL
{
    class RegionDB
    {
        private IConfiguration Configuration { get; }

        public RegionDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
