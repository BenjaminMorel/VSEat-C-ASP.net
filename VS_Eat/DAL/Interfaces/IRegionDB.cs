using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRegionDB
    {

        List<Region> GetAllRegions();

        string GetRegionName(int IdRegion); 
    }
}
