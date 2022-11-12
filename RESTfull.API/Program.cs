using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RESTfull.API;
using RESTfull.Infrastructure;

Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddConsole();
    })
    .Build()
    .Run();