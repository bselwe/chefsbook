
using System.Threading.Tasks;

namespace ChefsBook.Auth.Services
{
    public interface IAccountService
    {
        Task SignUp(string firstName, string lastName, string email, string password);
    }
}