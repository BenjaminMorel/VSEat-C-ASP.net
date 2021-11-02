
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class RegionManager : IRegionManager
    {
        private IRegionDB RegionDB { get; }
        public RegionManager(IConfiguration configuration)
        {
            RegionDB = new RegionDB(configuration); 
        }
    }
}
