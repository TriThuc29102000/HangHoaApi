using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangHoaApi
{
    public class Program
    {
        /*
         Host (IHost) object:
            - Dependency Injection (ID) : IServiceProvider (ServiceCollertion)
            - Logging (ILogging)
            - Configiraton
            - IHostedService => StarAsync : Run HTTP Server (Kestrel Http)

        1) T?o IHostBuilder
        2) Cau hinh, dang ki cac dich vu ( goi ConfigureWebHostDefaults)
        3) IHostBuilder.Build() => Host( IHost)
        4) Host.Run()

        Request => pipeline(Middleware)
        */
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
