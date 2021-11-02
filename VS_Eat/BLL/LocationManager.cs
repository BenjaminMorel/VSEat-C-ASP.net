
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class LocationManager : ILocationManager
    {
        private ILocationDB LocationDB { get;  }
        public LocationManager(IConfiguration configuration)
        {
            LocationDB = new LocationDB(configuration); 
        }
    }
}
