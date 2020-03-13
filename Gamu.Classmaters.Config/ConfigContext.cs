using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Gamu.Classmaters.Config
{
    public class CofigurationContext
    {
        public static ClassmatersConfig Current()
        {
            var builder = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json");

            var config = builder.Build();

            var appConfig = config.GetSection("ClassmatersConfig")
                .Get<ClassmatersConfig>();

            return appConfig;
        }
    }
}
