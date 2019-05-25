using Microsoft.Extensions.Hosting;
using System;

namespace Bookify.Auth.Infrastructure.Helpers
{
    public static class EnvironmentHelper
    {
        public const string EnviromentVariable = "ASPNETCORE_ENVIRONMENT";

        /// <summary>
        /// Gets current environment name
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentEnvironmentVariableName()
        {
            var environment = Environment.GetEnvironmentVariable(EnviromentVariable);
            if (environment == null || !(environment == EnvironmentName.Development || environment == EnvironmentName.Production))
            {
                throw new InvalidOperationException("Invalid environment. Please specify correct environment.");
            }

            return environment;
        }
    }
}
