
using System.Threading.Tasks;
using ChefsBook.Auth.Google;

namespace ChefsBook.Auth.Services
{
    public interface IAccountService
    {
        Task<AuthUser> GetUserInfo(string userId);
        Task SignUp(string firstName, string lastName, string email, string password);
        Task<GoogleSignUpResult> SignUpWithGoogle(GoogleUser googleUser);
    }
}