using System;
using Microsoft.AspNetCore.Identity;

namespace ChefsBook.Auth
{
    public class AuthUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
    }
}
