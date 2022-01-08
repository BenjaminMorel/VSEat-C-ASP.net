using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;

namespace BLL
{
    public class DeliveryStaffManager : IDeliveryStaffManager
    {
        private IDeliveryStaffDB DeliveryStaffDb { get; }

        private ILoginDB LoginDB { get;  }
        
        private ILocationDB LocationDB { get; }

        private ILoginManager LoginManager { get;  }

        private IRegionManager RegionManager { get; }


        public DeliveryStaffManager(IDeliveryStaffDB DeliveryStaffDb, ILoginDB LoginDB, ILocationDB LocationDB, ILoginManager LoginManager, IRegionManager RegionManager)
        {
            this.DeliveryStaffDb = DeliveryStaffDb;
            this.LoginDB = LoginDB;
            this.LocationDB = LocationDB;
            this.LoginManager = LoginManager;
            this.RegionManager = RegionManager;
        }

        public List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff)
        {
            return DeliveryStaffDb.CountOpenOrderByStaffID(IdDeliveryStaff); 
        }

        public List<DeliveryStaff> GetAllDeliveryStaff()
        {
            return DeliveryStaffDb.GetAllDeliveryStaff();
        }

        public DeliveryStaff CreateNewStaff(string FirstName, string LastName, string PhoneNumber, string Address, int PostCode, string City, string RegionName, string Email,
            string Password)
        {
            if (LoginManager.EmailVerification(Email))
            {
                Console.WriteLine("THIS EMAIL IS ALREADY USE BY AN OTHER ACCOUNT\nPLEASE CONNECT HERE");
            }

            var Location = new Location();
            Location = LocationDB.GetLocation(PostCode, City);

            int region = RegionManager.GetIdRegion(RegionName);

            var MyLogin = new Login();
            MyLogin.Password = Password;
            MyLogin.Username = Email;
            MyLogin.IdLoginType = 3; // The idLoginType for Staff will always be 3
            MyLogin = LoginDB.AddNewLogin(MyLogin);

            var MyDeliveryStaff = new DeliveryStaff();
            MyDeliveryStaff.FirstName = FirstName;
            MyDeliveryStaff.LastName = LastName;
            MyDeliveryStaff.PhoneNumber = PhoneNumber;
            MyDeliveryStaff.Address = Address;
            MyDeliveryStaff.IdLogin = MyLogin.IdLogin;
            MyDeliveryStaff.IdLocation = Location.IdLocation;
            MyDeliveryStaff.IdDeliveryStaffType = 1; // For validation
            MyDeliveryStaff.IdWorkingRegion = region;
            MyDeliveryStaff = DeliveryStaffDb.AddStaff(MyDeliveryStaff);

            return MyDeliveryStaff; 

        }

        public DeliveryStaff UpdateDeliveryStaff(DeliveryStaff myDeliveryStaff)
        {
            return DeliveryStaffDb.UpdateDeliveryStaff(myDeliveryStaff);
        }

        public DeliveryStaff GetDeliveryStaffByID(int IdLogin)
        {
            return DeliveryStaffDb.GetDeliveryStaffByID(IdLogin);
        }

        public DeliveryStaff GetDeliveryStaffByIDStaff(int IdDeliveryStaff)
        {
            return DeliveryStaffDb.GetDeliveryStaffByIDStaff(IdDeliveryStaff);
        }

        public List<DeliveryStaff> FindStaffFororder(int IdRegion)
        {
            return DeliveryStaffDb.FindStaffFororder(IdRegion); 
        }

    }
}
