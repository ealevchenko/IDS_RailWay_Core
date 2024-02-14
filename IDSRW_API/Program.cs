using EF_IDS.Concrete;
using EF_IDS.Concrete.Directory;
using EF_IDS.Entities;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using WebAPI.Repositories;
using WebAPI.Repositories.Arrival;
using WebAPI.Repositories.GIVC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IConfiguration Configuration = builder.Configuration;
var connectionString = Configuration["ConnectionStrings:IDS"];

builder.Services.AddDbContext<EFDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddScoped<ILongRepository<ArrivalCar>, ArrivalCarRepository>();
builder.Services.AddScoped<ILongRepository<ArrivalSostav>, ArrivalSostavRepository>();
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
