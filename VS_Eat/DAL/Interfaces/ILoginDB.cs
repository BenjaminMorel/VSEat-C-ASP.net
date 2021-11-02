

using DTO;

namespace DAL.Interfaces
{
    public interface ILoginDB
    { 
        void ShowAllLogin();
        
        Login GetLogin(string Email, string Password);

        bool EmailVerification(string Email); 
    }
}
