using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;
using IdentityServer4.Postgresql.Mappers;
using System.Collections.Generic;
using static IdentityServer4.Models.IdentityResources;

namespace Bookify.Auth.Configuration
{
    public static class DataSeed
    {
        public static IEnumerable<PostgresApiResource> GetApiResources()
        {
            return new List<PostgresApiResource>
            {
                new PostgresApiResource
                {
                    Name = "bookifyApi",
                    Description = "Bookify API",
                    DisplayName = "Bookify API",
                    Scopes = new List<PostgresApiScope>
                    {
                        new PostgresApiScope
                        {
                            Name = "bookifyApi",
                            DisplayName = "Bookify API"
                        }
                    }
                }
            };
        }

        public static IEnumerable<PostgresClient> GetClients()
        {
            return new List<PostgresClient>
            {
                new PostgresClient
                {
                    AllowOfflineAccess = true,
                    Id = "bookifyApi",
                    ClientId = "bookifyApi",
                    ClientName = "Bookify API",
                    AllowedGrantTypes =  new List<PostgresClientGrantType>
                    {
                        new PostgresClientGrantType
                        {
                            GrantType = GrantType.ClientCredentials
                        }
                    },
                    AllowedCorsOrigins =  new List<PostgresClientCorsOrigin>
                    {
                        new PostgresClientCorsOrigin
                        {
                            Origin = "https://localhost:5001"
                        }
                    },
                    RequireClientSecret = true,
                    ClientSecrets = new List<PostgresClientSecret>
                    {
                        new PostgresClientSecret
                        {
                            Value = "secret".Sha256()
                        }
                    },
                    RequireConsent = false,
                    AllowedScopes = new List<PostgresClientScope>
                    {
                        new PostgresClientScope
                        {
                            Scope = IdentityServerConstants.StandardScopes.OpenId
                        },
                        new PostgresClientScope
                        {
                            Scope = IdentityServerConstants.StandardScopes.Profile
                        },
                        new PostgresClientScope
                        {
                            Scope = "bookifyApi"
                        }
                    },
                    RedirectUris = new List<PostgresClientRedirectUri>
                    {
                        new PostgresClientRedirectUri
                        {
                            RedirectUri = "https://localhost:5001/signin-oidc"
                        }
                    }
                }
            };
        }

        public static IEnumerable<PostgresIdentityResource> GetIdentityResources()
        {
            return new List<PostgresIdentityResource>
            {
                new OpenId().ToEntity(),
                new Profile().ToEntity(),
                new Email().ToEntity(),
                new Phone().ToEntity()
            };
        }
    }
}
