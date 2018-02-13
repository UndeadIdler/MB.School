using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MB.School.EntityFramework;
using MB.School.EntityFramework.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MB.School
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host=BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                try
                {
                    var context = service.GetRequiredService<SchoolDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception e)
                {
//                    Console.WriteLine(e);
//                    throw new Exception(e.Message,"初始化错误");

                    var logger = service.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e,"初始化系统测试数据时报错，请联系管理员.");

                }

            }
            

            host.Run();

//            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
