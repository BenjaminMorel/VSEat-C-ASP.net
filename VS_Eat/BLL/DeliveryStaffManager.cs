using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DTO;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.Configuration;

namespace BLL
{
    public class DeliveryStaffManager : IDeliveryStaffManager
    {
        private IDeliveryStaffDB DeliveryStaffDb { get; }
        private ILoginDB LoginDB { get;  }
        
        private ILocationDB LocationDB { get; }

        private ILoginManager LoginManager { get;  }

        public DeliveryStaffManager(IConfiguration configuration)
        {
            DeliveryStaffDb = new DeliveryStaffDB(configuration);
            LoginDB = new LoginDB(configuration);
            LocationDB = new LocationDB(configuration); 

            LoginManager = new LoginManager(configuration); 
        }

        public List<Order> CountOpenOrderByStaffID(int IdDeliveryStaff)
        {
            return DeliveryStaffDb.CountOpenOrderByStaffId(IdDeliveryStaff); 
        }

        public DeliveryStaff CreateNewStaff(string FirstName, string LastName, string PhoneNumber, int PostCode, string City, string Email,
            string Password)
        {
            if (LoginManager.EmailVerification(Email))
            {
                Console.WriteLine("THIS EMAIL IS ALREADY USE BY AN OTHER ACCOUNT\nPLEASE CONNECT HERE");
            }

            var Location = new Location();
            Location = LocationDB.GetLocation(PostCode, City);

            var MyLogin = new Login();
            MyLogin.Password = Password;
            MyLogin.Username = Email;
            // The idLoginType for Staff will always be 3
            MyLogin.IdLoginType = 3;

            MyLogin = LoginDB.AddNewLogin(MyLogin);

            var MyDeliveryStaff = new DeliveryStaff();

            MyDeliveryStaff.IdLogin = MyLogin.IdLogin;
            MyDeliveryStaff.IdLocation = Location.IdLocation;
            MyDeliveryStaff.FirstName = FirstName;
            MyDeliveryStaff.LastName = LastName;
            MyDeliveryStaff.PhoneNumber = PhoneNumber;

            MyDeliveryStaff = DeliveryStaffDb.AddDeliveryStaff(MyDeliveryStaff);

            return MyDeliveryStaff; 

        }

    }
}
