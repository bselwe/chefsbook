using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;

namespace AuthApp.Configuration
{
    using ChefsBook.Auth.Contracts;
    using ChefsBook.Auth.Security;
    using static IdentityServer4.IdentityServerConstants;

    static class ISConfiguration
    {
        public static List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = AuthConsts.ChefsBookManagerId,
                    ClientName = "ChefsBook Manager",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    RequireClientSecret = false,

                    AllowedScopes =
                    {
                        StandardScopes.OpenId,
                        StandardScopes.Email,
                        AuthConsts.ChefsBookManagementApiScope
                    }
                }
            };
        }

        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static List<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(AuthConsts.ChefsBookManagementApiScope, new string[]
                {
                    KnownClaims.Role
                })
            };
        }
    }
}
