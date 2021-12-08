

using System.Collections.Generic;
using DTO;

namespace DAL.Interfaces
{
    public interface ILocationDB
    {
        Location GetLocation(int PostCode, string City);

        Location GetLocationByID(int IdLocation); 
        List<Location> GetAllLocations(); 



    }
}
