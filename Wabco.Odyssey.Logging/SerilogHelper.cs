using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using Serilog.Formatting.Json;
using System;
using System.Reflection;

namespace Wabco.Odyssey.Logging
{
    public static class SerilogHelper
    {
       public static void WithConfiguration(this LoggerConfiguration loggerConfig,
       string applicationName, IConfiguration config)
        {
            var name = Assembly.GetEntryAssembly().GetName();

            loggerConfig
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", $"{name.Name}")
                .Enrich.WithProperty("Version", $"{name.Version}")
                .WriteTo.File(new JsonFormatter (),
                 $@"C:\temp\Logs\{applicationName}.json");

        }
    }
}
