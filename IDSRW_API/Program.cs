using EF_IDS.Concrete;
using EF_IDS.Concrete.Directory;
using EF_IDS.Entities;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;
using WebAPI.Repositories.Arrival;
using WebAPI.Repositories.Directory;
using WebAPI.Repositories.GIVC;
using NLog;
using NLog.Web;
using System.Text.Json.Serialization;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{

    var builder = WebApplication.CreateBuilder(args);
    IConfiguration Configuration = builder.Configuration;
    var connectionString = Configuration["ConnectionStrings:IDS"];
    builder.Services.AddDbContext<EFDbContext>(x => x.UseSqlServer(connectionString));
    // Add services to the container.
    builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // игнорируем циклические сылки
    });
    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<ILongRepository<ArrivalCar>, ArrivalCarRepository>();
    builder.Services.AddScoped<ILongRepository<ArrivalSostav>, ArrivalSostavRepository>();
    //builder.Services.AddScoped<IRepository<DirectoryCargoEtsng>, DirectoryCargoEtsngRepository>();
    //builder.Services.AddScoped<IRepository<DirectoryCargo>, DirectoryCargoRepository>();
    //builder.Services.AddScoped<IRepository<DirectoryCargoGroup>, DirectoryCargoGroupRepository>();
    builder.Services.AddScoped<IRepository<GivcRequest>, GIVCRepository>();


    //builder.Services.AddScoped<EF_IDS.Abstract.IRepository<GivcRequest>, EFGivcRequest>();


    //builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    //   .AddNegotiate();

    //builder.Services.AddAuthorization(options =>
    //{
    //    // By default, all incoming requests will be authorized according to the default policy.
    //    options.FallbackPolicy = options.DefaultPolicy;
    //});



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    //if (app.Environment.IsDevelopment())
    //{

    //}
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

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