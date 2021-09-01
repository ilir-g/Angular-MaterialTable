using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenetecDomain_IlirG;
using GenetecWebsite_IlirG;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GenetecWebSite_IlirG
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<Genetec_IlirGContext>();
                if (db != null)
                    db.Database.Migrate();
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
             .ConfigureLogging((hostingContext, logging) =>
             {
                 logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                 logging.AddConsole();
                 logging.AddDebug();
                 logging.AddEventSourceLogger();
             })
             .UseStartup<Startup>();
    }
}
