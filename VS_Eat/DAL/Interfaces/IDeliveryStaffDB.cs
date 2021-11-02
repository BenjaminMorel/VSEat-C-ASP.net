
namespace DAL.Interfaces
{
    public interface IDeliveryStaffDB
    {
        int CountOpenOrderByStaffID(int IdDeliveryStaff);

        void CreateNewStaff(string FirstName, string Name, int PostCode, string City, string Email,
            string Password);

    }
}
