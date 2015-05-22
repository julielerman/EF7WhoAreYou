using System;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

namespace EF7Samurai.Context
{
    public static class ExtensionMethods
    {
        public static void LogToConsole(this SamuraiContext context)
        {
            // IServiceProvider represents registered DI container
            IServiceProvider contextServices = ((IDbContextServices)context).ScopedServiceProvider;

            // Get the registered ILoggerFactory from the DI container
            var loggerFactory = contextServices.GetRequiredService<ILoggerFactory>();

            // Add a logging provider with a console trace listener
            loggerFactory.AddConsole(LogLevel.Verbose);
        }
    }
}
