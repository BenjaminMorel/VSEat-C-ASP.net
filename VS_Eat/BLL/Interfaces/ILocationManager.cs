using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BLL.Interfaces
{
    public interface ILocationManager
    {
        List<Location> GetAllLocations();

        Location GetLocation(string City, int PostCode);

        Location GetLocationByID(int IdLocation); 
    }
}
