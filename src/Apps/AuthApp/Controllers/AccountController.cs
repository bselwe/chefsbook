using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ChefsBook.Auth.Contracts;
using ChefsBook.Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(
            IAccountService accountService
        )
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO user)
        {
            await accountService.SignUp(user.FirstName, user.LastName, user.Email, user.Password);
            return new StatusCodeResult((int) HttpStatusCode.Created);
        }
    }
}
