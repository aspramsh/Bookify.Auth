using IdentityServer4.Models;
using IdentityServer4.Postgresql.Entities;
using IdentityServer4.Postgresql.Extensions;
using IdentityServer4.Postgresql.Mappers;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using ApiResource = IdentityServer4.Postgresql.Entities.ApiResource;
using Client = IdentityServer4.Postgresql.Entities.Client;
using IdentityResource = IdentityServer4.Postgresql.Entities.IdentityResource;

namespace Bookify.Auth
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            var connectionString = Configuration.GetConnectionString("IdentityServerConnection");
            services.AddIdentityServer().AddConfigurationStore(connectionString).AddOperationalStore().AddDeveloperSigningCredential();

            services.AddScoped<IDocumentStore>(provider => DocumentStore.For(connectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            InitData(app);

            loggerFactory.AddConsole();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
        }

        private void InitData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var store = serviceScope.ServiceProvider.GetRequiredService<IDocumentStore>();
                store.Advanced.Clean.CompletelyRemoveAll();
                using (var session = store.LightweightSession())
                {
                    if (!session.Query<ApiResource>().Any())
                    {
                        var resources = new List<ApiResource> {
                     new ApiResource{ Name = "bookifyApi" , Description = "Bookify API" , DisplayName ="Bookify API" , Scopes = new List<ApiScope> { new ApiScope { Name = "bookifyApi", DisplayName = "Bookify API" } } },

                    };
                        session.StoreObjects(resources);
                    }

                    if (!session.Query<IdentityResource>().Any())
                    {
                        var resources = new List<IdentityResource> {
                        new IdentityResources.OpenId().ToEntity(),
                        new IdentityResources.Profile().ToEntity(),
                        new IdentityResources.Email().ToEntity(),
                        new IdentityResources.Phone().ToEntity()
                    };
                        session.StoreObjects(resources);
                    }
                    if (!session.Query<Client>().Any())
                    {
                        var clients = new List<Client>
                    {
                        new Client
                        {
                            AllowOfflineAccess = true,
                            Id = "bookifyApi",
                            ClientId = "bookifyApi",
                            ClientName = "Bookify API",
                            AllowedGrantTypes =  new List<ClientGrantType> { new ClientGrantType { GrantType = GrantType.ClientCredentials } },
                            AllowedCorsOrigins =  new List<ClientCorsOrigin>  {new ClientCorsOrigin { Origin = "https://localhost:5001" } },
                            RequireClientSecret = true,
                            ClientSecrets = new List<ClientSecret> { new ClientSecret { Value = "secret".Sha256() }  },
                            RequireConsent = false,
                            AllowedScopes = new List<ClientScope> {
                                new ClientScope { Scope = IdentityServer4.IdentityServerConstants.StandardScopes.OpenId },
                                new ClientScope { Scope = IdentityServer4.IdentityServerConstants.StandardScopes.Profile },
                                new ClientScope { Scope = "bookifyApi" }
                            },
                            RedirectUris = new List<ClientRedirectUri> { new ClientRedirectUri { RedirectUri ="https://localhost:5001/signin-oidc" }
                            }
                        }
                    };
                        session.StoreObjects(clients);
                    }
                    session.SaveChanges();
                }
            }
        }
    }
}
