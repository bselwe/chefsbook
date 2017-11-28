

using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System;
using ChefsBook.Auth.Security;
using System.Security.Claims;

namespace ChefsBook.Auth.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AuthUser> userManager;

        public AccountService(
            UserManager<AuthUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SignUp(string firstName, string lastName, string email, string password)
        {
            var id = Guid.NewGuid();
            var user = new AuthUser
            {
                Id = id,
                Email = email,
                UserName = email,
                FirstName = firstName,
                LastName = lastName,
            };
            
            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                throw new InvalidOperationException("Cannot create user. ASP.NET Core Identity rejected the request.");
            
            await userManager.AddClaimAsync(user, new Claim(KnownClaims.Role, KnownRoles.User));
        }
    }
}