﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    class IDeliveryStaffDB
    {
        private IDeliveryStaffDB DeliveryStaffDb { get; }

        public IDeliveryStaffDB(IConfiguration configuration)
        {
         //   DeliveryStaffDb = new DeliveryStaffDB(configuration);
        }
    }
}
