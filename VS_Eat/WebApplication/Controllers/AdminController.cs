using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using WebApplication.Models;
using DTO;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class AdminController : Controller
    {

        private IDeliveryStaffManager deliveryStaffManager { get; }

        private IRegionManager regionManager { get; }

        private IDeliveryStaffTypeManager deliveryStaffTypeManager { get; }

        public AdminController(IDeliveryStaffManager deliveryStaffManager, IRegionManager regionManager, IDeliveryStaffTypeManager deliveryStaffTypeManager)
        {
            this.deliveryStaffManager = deliveryStaffManager;
            this.regionManager = regionManager;
            this.deliveryStaffTypeManager = deliveryStaffTypeManager;
        }

        /// <summary>
        /// Method to see all the staff that are working and the old one that not work here anymore
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<DeliveryStaff> allStafs = deliveryStaffManager.GetAllDeliveryStaff();

            List<StaffToDisplay> listOfAllStaffs = new List<StaffToDisplay>();

            foreach(var deliveryStaff in allStafs)
            {
                var region = regionManager.GetRegionName(deliveryStaff.IdWorkingRegion);
             
                var deliveryStaffType = deliveryStaffTypeManager.GetAllDeliveryStaffType(deliveryStaff.IdDeliveryStaffType);
                StaffToDisplay staffToDisplay = new StaffToDisplay(deliveryStaff.FirstName, deliveryStaff.LastName, deliveryStaff.PhoneNumber, region.RegionName,deliveryStaff.IdDeliveryStaffType,deliveryStaffType.DeliveryStaffTypeStr);

                listOfAllStaffs.Add(staffToDisplay);
            }

            return View(listOfAllStaffs);
        }
    }
}
