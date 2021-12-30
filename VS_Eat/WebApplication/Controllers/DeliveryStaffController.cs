using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class DeliveryStaffController : Controller
    {

        private IDeliveryStaffManager deliveryStaffManager { get; }
        private IOrderManager orderManager { get; }
        private IRegionManager regionManager { get; }

        public DeliveryStaffController(IDeliveryStaffManager deliveryStaffManager, IOrderManager orderManager, IRegionManager regionManager)
        {
            this.deliveryStaffManager = deliveryStaffManager;
            this.orderManager = orderManager;
            this.regionManager = regionManager;
        }

        public IActionResult Index()
        {
            var IdStaff = (int) HttpContext.Session.GetInt32("ID_LOGIN");

            var myDeliveryStaff = deliveryStaffManager.GetDeliveryStaffByID(IdStaff);
            var myRegion = regionManager.GetRegionName(myDeliveryStaff.IdWorkingRegion);

            var allOrders = deliveryStaffManager.CountOpenOrderByStaffID(myDeliveryStaff.IdDeliveryStaff);
            
            return View(allOrders);

        }

    }
}
