using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostserver = CreateHostBuilder(args).Build();
            using (var ambient = hostserver.Services.CreateScope()){
                var service = ambient.ServiceProvider;

                try
                {
                    var context = service.GetRequiredService<RebStoreContext>();
                    context.Database.Migrate();
                }catch(Exception e){
                    var loggin = service.GetRequiredService<ILogger<Program>>();
                    loggin.LogError(e,"Error in  the migration");
                }
                
            }
            hostserver.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
