﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    class Region
    {
        public int IdRegion { get; set; }
        public string RegionName { get; set; }
        public override string ToString()
        {
            return "\n__________________________\n" +
                   "\nIdRegion : " + IdRegion +
                   "\nRegionName : " + RegionName +
                   "\n__________________________\n";
        }
    }
}