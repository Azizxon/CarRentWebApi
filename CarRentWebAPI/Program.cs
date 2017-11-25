using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CarRentWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug().WriteTo.RollingFile("log-{Date}.log").CreateLogger();
            try
            {
                Log.Information("starting services...");
                BuildWebHost(args).Run();
            }
            catch (Exception exception)
            {
              Log.Fatal(exception,"Failed to start services");
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
            .UseSerilog()
                .Build();
    }
}
