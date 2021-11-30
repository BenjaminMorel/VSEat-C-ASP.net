
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
        private IRegionDB RegionDb { get; }

        public RegionManager(IRegionDB regionDb)
        {
            this.RegionDb = regionDb;
        }

        public List<Region> GetAllRegions()
        {
            return RegionDb.GetAllRegions();
        }
    }
}
