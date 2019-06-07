using AutoMapper;
using Bookify.Auth.Business.ProfileValidator;
using Bookify.Auth.DataAccess.DbContexts;
using Bookify.Auth.DataAccess.Initialize;
using Bookify.Auth.Infrastructure.Helpers.Http;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Bookify.Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AuthDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("IdentityServerConnection")));

            var bookifyApiAddress = Configuration.GetValue<string>("BookifyApiAddress");
            services.AddSingleton<IHttpClientHelper, HttpClientHelper>(client => new HttpClientHelper(bookifyApiAddress));

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>()
                .AddTransient<IProfileService, ProfileService>();

            #region swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Bookify Auth", Version = "v1" });
            });
            #endregion

            services.AddIdentityServer()
                    .AddDeveloperSigningCredential()
                    .AddConfigurationStore(option =>
                           option.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("IdentityServerConnection"), options =>
                           options.MigrationsAssembly("Bookify.Auth.DataAccess")))
                    .AddOperationalStore(option =>
                           option.ConfigureDbContext = builder => builder.UseNpgsql(Configuration.GetConnectionString("IdentityServerConnection"), options =>
                           options.MigrationsAssembly("Bookify.Auth.DataAccess")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, AuthDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            #region swagger
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookify Auth V1");
            });

            #endregion

            app.UseIdentityServer();
            app.UseHttpsRedirection();
            app.UseMvc();

            DatabaseInitializer.Initialize(app, context);
        }
    }
}
