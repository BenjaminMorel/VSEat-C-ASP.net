using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IRegionManager
    {

        List<Region> GetAllRegions();

        string GetRegionName(int IdRegion); 

    }
}
