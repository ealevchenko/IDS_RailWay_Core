using System.Reflection;
using WS_IDS;
using NLog;
using NLog.Extensions.Logging;

System.Environment.CurrentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
ILogger<Program> logger = LoggerFactory.Create(builder => builder.AddNLog()).CreateLogger<Program>();

try
{
    IHost host = Host.CreateDefaultBuilder(args)
        .UseWindowsService()
        .ConfigureServices(services =>
        {
            services.AddHostedService<UpdateBankRate>();
            services.AddHostedService<UpdateRent>();
            services.AddHostedService<UpdateGIVC>();
        }).ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            //logging.AddConsole();
            //logging.AddDebug();
            logging.AddNLog();
        })
        .Build();

    await host.RunAsync();
}
catch (Exception ex)
{
    logger.LogError(0, ex, "Error WS_IDS");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    LogManager.Shutdown();
}



