

using DTO;

namespace DAL.Interfaces
{
    public interface ILocationDB
    {
        Location GetLocation(int PostCode, string City);

        //TODO GetAllLocations
    }
}
