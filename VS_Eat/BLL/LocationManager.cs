
using System.Collections.Generic;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Location = DTO.Location;

namespace BLL
{
    public class LocationManager : ILocationManager
    {
        private ILocationDB LocationDB { get;  }
        public LocationManager(ILocationDB LocationDB)
        {
            this.LocationDB = LocationDB;
        }

        public Location GetLocation(string City, int PostCode)
        {
            return LocationDB.GetLocation(PostCode, City); 
        }

        public Location GetLocationByID(int IdLocation)
        {
            return LocationDB.GetLocationByID(IdLocation); 
        }
        public List<Location> GetAllLocations()
        {
            return LocationDB.GetAllLocations(); 
        }
    }
}
