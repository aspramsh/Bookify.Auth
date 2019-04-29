using Bookify.Auth.Configuration;
using IdentityServer4.Postgresql.Extensions;
using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
                        var resources = DataSeed.GetApiResources();
                        session.StoreObjects(resources);
                    }

                    if (!session.Query<IdentityResource>().Any())
                    {
                        var resources = DataSeed.GetIdentityResources();
                        session.StoreObjects(resources);
                    }
                    if (!session.Query<Client>().Any())
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
