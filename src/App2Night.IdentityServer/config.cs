using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Services.InMemory;

namespace App2Night.IdentityServer
{
    public class Config
    {
        public static IEnumerable<Scope> GetScopes()
        {
            return new List<Scope>
            {
                new Scope
                {
                    Name = "api1",
                    Description = "My API",
                    IncludeAllClaimsForUser = true,
                },
                new Scope()
                {
                    Name = "identity",
Type = ScopeType.Resource,

    Claims = new List<ScopeClaim>
        {
              new ScopeClaim("role"),
              new ScopeClaim(ClaimTypes.Name),
              new ScopeClaim(ClaimTypes.GivenName)
        },

    IncludeAllClaimsForUser = true
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "nativeApp",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = {"api1", "identity", StandardScopes.Profile.Name, StandardScopes.OpenId.Name, "openid"}
                }
            };
        }


        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "1",
                    Username = "alice",
                    Password = "password" 
                },
                new InMemoryUser
                {
                    Subject = "2",
                    Username = "bob",
                    Password = "password",
                    Claims = new []
                    {
                        new Claim(ClaimTypes.Name, "Bobss"), 
                        new Claim(ClaimTypes.GivenName, "Testname")
                    }
                }
            };
        }
    }
}