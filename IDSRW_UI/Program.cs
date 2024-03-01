using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Xml.Linq;
using NLog;
using NLog.Web;

//System.Environment.CurrentDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    //// Configure the HTTP request pipeline.
    //if (!app.Environment.IsDevelopment())
    //{
    //    app.UseExceptionHandler("/Home/Error");
    //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //    app.UseHsts();
    //}

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseAuthorization();

    //app.UsePathBase("/IDSRW_UI");

    //app.MapControllers();

    //app.UseRouting(routes =>
    //{
    //    routes.MapControllerRoute(
    //        name: "default",
    //        pattern: "{controller=Home}/{action=Index}/{id?}",
    //        defaults: new { controller = "Home", action = "Index" },
    //        constraints: new { id = new IntRouteConstraint() },
    //        dataTokens: new { pathBase = "/app1" }
    //    );
    //});


    //app.UseRouting();

    //app.UseEndpoints(endpoints =>
    //{
    //    endpoints.MapControllerRoute(
    //        name: "MyArea",
    //      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    //    endpoints.MapControllerRoute(
    //        name: "default",
    //        pattern: "{controller=Home}/{action=Index}/{id?}");
    //});


    //app.UseAuthorization();

    app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}",
            defaults: new { controller = "Home", action = "Index" },
            dataTokens: new { pathBase = "/IDSRW_UI" });
    //app.MapAreaControllerRoute(
    //    name: "areas",
    //    areaName: "default",
    //    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    //    );

    //app.UseRouting();
    //app.UseEndpoints(endpoints =>
    //{
    //    endpoints.MapControllerRoute(
    //      name: "areas",
    //      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    //    endpoints.MapControllerRoute(
    //      name: "default",
    //      pattern: "{controller=Home}/{action=Index}/{id?}");
    //});


    //app.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");
    app.Run();
}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}