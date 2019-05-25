using System;
using Bookify.Auth.Infrastructure.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Bookify.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Create default environment on machine level

            var environment = Environment.GetEnvironmentVariable(EnvironmentHelper.EnviromentVariable);

            if (environment == null)
            {
                Environment.SetEnvironmentVariable(EnvironmentHelper.EnviromentVariable, "Development");
            }

            #endregion
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
