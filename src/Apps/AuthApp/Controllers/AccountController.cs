using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ChefsBook.Auth.Contracts;
using ChefsBook.Auth.Security;
using ChefsBook.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChefsBook.AuthApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUserInfo()
        {
            var userId = User.FindFirstValue(KnownClaims.UserId);
            var user = await accountService.GetUserInfo(userId);

            var userInfo = new UserInfoDTO()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Photo = user.Photo
            };

            return Ok(userInfo);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO user)
        {
            await accountService.SignUp(user.FirstName, user.LastName, user.Email, user.Password);
            return new StatusCodeResult((int) HttpStatusCode.Created);
        }
    }
}
