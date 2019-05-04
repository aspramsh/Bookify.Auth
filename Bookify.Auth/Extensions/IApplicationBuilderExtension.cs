using Bookify.Auth.Configuration;
using IdentityServer4.Postgresql.Entities;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Bookify.Auth.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static void InitData(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var store = serviceScope.ServiceProvider.GetRequiredService<IDocumentStore>();
                store.Advanced.Clean.CompletelyRemoveAll();
                using (var session = store.LightweightSession())
                {
                    if (!session.Query<PostgresApiResource>().Any())
                    {
                        var resources = DataSeed.GetApiResources();
                        session.StoreObjects(resources);
                    }

                    if (!session.Query<PostgresIdentityResource>().Any())
                    {
                        var resources = DataSeed.GetIdentityResources();
                        session.StoreObjects(resources);
                    }
                    if (!session.Query<PostgresClient>().Any())
                    {
                        var clients = DataSeed.GetClients();
                        session.StoreObjects(clients);
                    }

                    session.SaveChanges();
                }
            }
        }
    }
}
