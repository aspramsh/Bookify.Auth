using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Bookify.Auth.Configuration
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources()
        {
            return new []
            {
                new ApiResource("bookifyApi", "Bookify API")
            };
        }

        public static IEnumerable<Client> Clients()
        {
            return new []
            {
                new Client
                {
                    ClientId = "bookifyApi",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "bookifyApi" }
                }
            };
        }

        public static IEnumerable<TestUser> Users()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "mail@aspramsh.am",
                    Password = "2019DevOpp!"
                }
            };
        }
    }
}
