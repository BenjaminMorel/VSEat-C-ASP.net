
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BLL
{
    public class RegionManager : IRegionManager
    {
        private IRegionDB RegionDB { get; }
        public RegionManager(IConfiguration configuration)
        {
            RegionDB = new RegionDB(configuration); 
        }

        public List<Region> GetAllRegions()
        {
            return RegionDB.GetAllRegions();
        }
    }
}
