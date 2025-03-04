using BusinessLayer.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
<<<<<<< HEAD
using BusinessLayer.Interface;
using BusinessLayer.Services;
=======
using Microsoft.OpenApi.Models;
>>>>>>> UC2
using NLog;
using NLog.Web;
using System;


    var builder = WebApplication.CreateBuilder(args);
<<<<<<< HEAD

    builder.Services.AddScoped<IGreetingBL,GreetingBL>();

    // ✅ Add NLog as the logging provider
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

=======
>>>>>>> UC2
    builder.Services.AddControllers();
    builder.Services.AddSingleton<IGreetingBL, GreetingBL>();
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