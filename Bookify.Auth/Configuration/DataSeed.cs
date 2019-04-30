using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;
using IdentityServer4.Postgresql.Mappers;
using System.Collections;
using System.Collections.Generic;
using ApiResource = IdentityServer4.Postgresql.Entities.ApiResource;
using Client = IdentityServer4.Postgresql.Entities.Client;
using IdentityResource = IdentityServer4.Postgresql.Entities.IdentityResource;

namespace Bookify.Auth.Configuration
{
    public static class DataSeed
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource
                {
                    Name = "bookifyApi",
                    Description = "Bookify API",
                    DisplayName = "Bookify API",
                    Scopes = new List<ApiScope>
                    {
                        new ApiScope
                        {
                            Name = "bookifyApi",
                            DisplayName = "Bookify API"
                        }
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    AllowOfflineAccess = true,
                    Id = "bookifyApi",
                    ClientId = "bookifyApi",
                    ClientName = "Bookify API",
                    AllowedGrantTypes =  new List<ClientGrantType>
                    {
                        new ClientGrantType
                        {
                            GrantType = GrantType.ClientCredentials
                        }
                    },
                    AllowedCorsOrigins =  new List<ClientCorsOrigin>
                    {
                        new ClientCorsOrigin
                        {
                            Origin = "https://localhost:5001"
                        }
                    },
                    RequireClientSecret = true,
                    ClientSecrets = new List<ClientSecret>
                    {
                        new ClientSecret
                        {
                            Value = "secret".Sha256()
                        }
                    },
                    RequireConsent = false,
                    AllowedScopes = new List<ClientScope>
                    {
                        new ClientScope
                        {
                            Scope = IdentityServer4.IdentityServerConstants.StandardScopes.OpenId
                        },
                        new ClientScope
                        {
                            Scope = IdentityServer4.IdentityServerConstants.StandardScopes.Profile
                        },
                        new ClientScope
                        {
                            Scope = "bookifyApi"
                        }
                    },
                    RedirectUris = new List<ClientRedirectUri>
                    {
                        new ClientRedirectUri
                        {
                            RedirectUri = "https://localhost:5001/signin-oidc"
                        }
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId().ToEntity(),
                new IdentityResources.Profile().ToEntity(),
                new IdentityResources.Email().ToEntity(),
                new IdentityResources.Phone().ToEntity()
            };
        }
    }
}
