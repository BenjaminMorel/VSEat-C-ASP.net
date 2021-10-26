

namespace DAL.Interfaces
{
    public interface ILoginDB
    { 
        void ShowAllLogin();

        int GetLogin(string Email, string Password);

        bool EmailVerification(string Email); 
    }
}
