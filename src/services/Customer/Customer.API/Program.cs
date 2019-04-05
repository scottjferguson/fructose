using Customer.API.Extensions;
using Customer.Infrastructure.ORM.EntityFramework;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.IO;

namespace Customer.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO: SF: Uncomment
            //CreateWebHostBuilder(args)
            //    .MigrateDbContext<FructoseContext>((context, services) =>
            //    {
            //        var env = services.GetService<IHostingEnvironment>();
            //        var settings = services.GetService<IOptions<OrderingSettings>>();
            //        var logger = services.GetService<ILogger<OrderingContextSeed>>();

            //        new OrderingContextSeed()
            //            .SeedAsync(context, env, settings, logger)
            //            .Wait();
            //    })
            //    .MigrateDbContext<IntegrationEventLogContext>((_, __) => { })
            //    .Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    var builtConfig = config.Build();

                    var configurationBuilder = new ConfigurationBuilder();

                    if (Convert.ToBoolean(builtConfig["UseVault"]))
                    {
                        configurationBuilder.AddAzureKeyVault(
                            $"https://{builtConfig["Vault:Name"]}.vault.azure.net/",
                            builtConfig["Vault:ClientId"],
                            builtConfig["Vault:ClientSecret"]);
                    }

                    configurationBuilder.AddEnvironmentVariables();

                    config.AddConfiguration(configurationBuilder.Build());
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .UseApplicationInsights()
                .UseSerilog((builderContext, config) =>
                {
                    // TODO: SF: Uncomment
                    //config
                    //    .MinimumLevel.Information()
                    //    .Enrich.FromLogContext()
                    //    .WriteTo.Console();
                })
                .Build();
        }
    }
}
