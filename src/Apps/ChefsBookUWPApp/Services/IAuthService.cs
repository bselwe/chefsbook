using System.Threading.Tasks;

namespace ChefsBook_UWP_App.Services
{
    public interface IAuthService
    {
        Task<string> SignIn();
    }
}
