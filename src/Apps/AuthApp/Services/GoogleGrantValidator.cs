using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ChefsBook.Auth.Services;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace ChefsBook.Auth.Google
{
    public class GoogleGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => GoogleConsts.GrantType;

        private readonly GoogleClient googleClient;
        private readonly UserManager<AuthUser> userManager;
        private readonly IAccountService accountService;

        public GoogleGrantValidator(
            GoogleClient googleClient, 
            UserManager<AuthUser> userManager,
            IAccountService accountService)
        {
            this.userManager = userManager;
            this.googleClient = googleClient;
            this.accountService = accountService;
        }

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var token = context.Request.Raw[GoogleConsts.GrantField];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    GoogleConsts.Errors.NoAssertion);
                return;
            }

            GoogleUser googleUser = null;
            try
            {
                googleUser = await googleClient.GetUser(token);
            }
            catch
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    GoogleConsts.Errors.InvalidAssertion);
                return;
            }

            var user = await userManager.FindByLoginAsync(GrantType, googleUser.Id);
            if (user == null)
            {
                Console.WriteLine($"Could not find user with id: {googleUser.Id}");
                var result = await accountService.SignUpWithGoogle(googleUser);
                if (result.IsSuccess)
                {
                    Console.WriteLine($"Successfully created user with id: {result.User.Id}");
                    user = result.User;
                }
                else
                {
                    Console.WriteLine($"Could not create user. Error: {result.ErrorString}");
                    HandleGoogleSignUpFailure(context, result.ErrorString);
                }
            }

            if (user != null)
            {
                context.Result = new GrantValidationResult(user.Id.ToString(), GrantType);
            }
        }

        private void HandleGoogleSignUpFailure(ExtensionGrantValidationContext context, string errors)
        {
            context.Result = new GrantValidationResult(
                GoogleConsts.Errors.CouldNotRegisterUser,
                errors                                
            );
        }
    }
}