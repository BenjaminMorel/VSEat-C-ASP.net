

namespace DAL.Interfaces
{
    public interface ILoginDB
    { 
        void ShowAllLogin();

        int GetLogin(string Email, string Password); 
    }
}
