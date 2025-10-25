using Villa_VillaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Villa_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using Villa_VillaAPI;
using Villa_VillaAPI.IRepository;
using Villa_VillaAPI.IRepository.Repository;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IVillaRepository, VillaRepository>();
builder.Services.AddScoped<IAPIService, APIService>();

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
    .WriteTo.File("log/villaLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger(); // logs in file each day

builder.Host.UseSerilog(); // when an instance of logger is requested, dependency injection will provide a serilog instance

builder.Services.AddControllers()
    .AddNewtonsoftJson();
   
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVillaService, VillaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
