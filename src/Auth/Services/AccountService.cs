using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ChefsBook.Auth.Contracts;
using ChefsBook.Auth.Google;
using ChefsBook.Auth.Security;
using Microsoft.AspNetCore.Identity;

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

        public Task<AuthUser> GetUserInfo(string userId)
        {
            return userManager.FindByIdAsync(userId);
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

        public async Task<GoogleSignUpResult> SignUpWithGoogle(GoogleUser googleUser)
        {
            var id = Guid.NewGuid();
            var user = new AuthUser
            {
                Id = id,
                Email = googleUser.Email,
                UserName = googleUser.Email,
                FirstName = googleUser.FirstName,
                LastName = googleUser.LastName,
                Photo = googleUser.Photo
            };

            var result = await userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await userManager.AddClaimAsync(user, new Claim(KnownClaims.Role, KnownRoles.User));
                await userManager.AddLoginAsync(user, new UserLoginInfo(
                    GoogleConsts.GrantType,
                    googleUser.Id,
                    "Google"
                ));

                return new GoogleSignUpResult()
                {
                    IsSuccess = true,
                    User = user
                };
            }
            else
            {
                return new GoogleSignUpResult()
                {
                    IsSuccess = false,
                    ErrorString = GetGoogleSignUpErrorString(result.Errors)
                };
            }
        }

        private string GetGoogleSignUpErrorString(IEnumerable<IdentityError> errors)
        {
            var stringBuilder = new StringBuilder();
            foreach (var error in errors)
            {
                stringBuilder.Append($"{error.Description},");
            }
            return stringBuilder.ToString();
        }
    }
}