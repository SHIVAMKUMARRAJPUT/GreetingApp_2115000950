using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BusinessLayer.Interface;
using RepositoryLayer.Services;
using RepositoryLayer.Interface;
using NLog;
using NLog.Web;
using System;
   

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();
    builder.Services.AddSingleton<IGreetingBL,GreetingBL>();
    builder.Services.AddSingleton<IGreetingRL,GreetingRL>();
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