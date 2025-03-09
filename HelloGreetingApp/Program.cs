using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BusinessLayer.Interface;
using NLog;
using NLog.Web;
using System;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using RepositoryLayer.Services;




    var builder = WebApplication.CreateBuilder(args);


    // Retrieve connection string
    var connectionString = builder.Configuration.GetConnectionString("GreetingAppDB");

    Console.WriteLine($"Connection String: {connectionString}"); // Debugging output

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("Connection string 'GreetingAppDB' not found in appsettings.json.");
    }


    builder.Services.AddControllers();
    // Register DbContext
    builder.Services.AddDbContext<GreetingAppContext>(options =>
    options.UseSqlServer(connectionString));
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();
    builder.Services.AddScoped<IGreetingBL,GreetingBL>();
    builder.Services.AddScoped<IUserBL, UserBL>();
    builder.Services.AddScoped<IUserRL, UserRL>();


    // ✅ Add NLog as the logging provider
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
   

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();