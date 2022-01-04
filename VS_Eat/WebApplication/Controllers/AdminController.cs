using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Interfaces;
using WebApplication.Models;
using DTO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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


        public IActionResult Index()
        {
            List<DeliveryStaff> allStafs = deliveryStaffManager.GetAllDeliveryStaff();

            List<StaffToDisplay> listOfAllStaffs = new List<StaffToDisplay>();

            foreach(var deliveryStaff in allStafs)
            {
                StaffToDisplay staffToDisplay = new StaffToDisplay();
                var region = regionManager.GetRegionName(deliveryStaff.IdWorkingRegion);
                var deliveryStaffType = deliveryStaffTypeManager.GetAllDeliveryStaffType(deliveryStaff.IdDeliveryStaffType);

                staffToDisplay.IdDeliveryStaff = deliveryStaff.IdDeliveryStaff;
                staffToDisplay.FirstName = deliveryStaff.FirstName;
                staffToDisplay.LastName = deliveryStaff.LastName;
                staffToDisplay.PhoneNumber = deliveryStaff.PhoneNumber;
                staffToDisplay.RegionName = region.RegionName;
                staffToDisplay.IdDeliveryStaffType = deliveryStaff.IdDeliveryStaffType;
                staffToDisplay.DeliveryStaffType = deliveryStaffType.DeliveryStaffTypeStr;

                listOfAllStaffs.Add(staffToDisplay);
            }

            return View(listOfAllStaffs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(int IdDeliveryStaff)
        {

            DeliveryStaff myDeliveryStaff = deliveryStaffManager.GetDeliveryStaffByIDStaff(IdDeliveryStaff);

            if (myDeliveryStaff.IdDeliveryStaffType == 1 || myDeliveryStaff.IdDeliveryStaffType == 2)
            {
                myDeliveryStaff.IdDeliveryStaffType += 1;
                deliveryStaffManager.UpdateDeliveryStaff(myDeliveryStaff);
            }

            List<DeliveryStaff> allStafs = deliveryStaffManager.GetAllDeliveryStaff();

            List<StaffToDisplay> listOfAllStaffs = new List<StaffToDisplay>();

            foreach (var deliveryStaff in allStafs)
            {
                StaffToDisplay staffToDisplay = new StaffToDisplay();
                var region = regionManager.GetRegionName(deliveryStaff.IdWorkingRegion);
                var deliveryStaffType = deliveryStaffTypeManager.GetAllDeliveryStaffType(deliveryStaff.IdDeliveryStaffType);

                staffToDisplay.IdDeliveryStaff = deliveryStaff.IdDeliveryStaff;
                staffToDisplay.FirstName = deliveryStaff.FirstName;
                staffToDisplay.LastName = deliveryStaff.LastName;
                staffToDisplay.PhoneNumber = deliveryStaff.PhoneNumber;
                staffToDisplay.RegionName = region.RegionName;
                staffToDisplay.IdDeliveryStaffType = deliveryStaff.IdDeliveryStaffType;
                staffToDisplay.DeliveryStaffType = deliveryStaffType.DeliveryStaffTypeStr;

                listOfAllStaffs.Add(staffToDisplay);
            }

            return View(listOfAllStaffs);
        }
    }
}
