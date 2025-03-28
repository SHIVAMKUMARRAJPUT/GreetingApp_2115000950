﻿using BusinessLayer.Interface;
using NLog;
using NLog.Web;
using System;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Contexts;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using BusinessLayer.Services;
using Middleware.JwtHelper;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Middleware.RabbitMQClient;
using StackExchange.Redis;

    var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    logger.Info("Starting application...");


    var builder = WebApplication.CreateBuilder(args);

    // ✅ Setup NLog for logging
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    // ✅ Retrieve connection string
    var connectionString = builder.Configuration.GetConnectionString("GreetingAppDB");

    if (string.IsNullOrEmpty(connectionString))
    {
        logger.Error("Connection string 'GreetingAppDB' not found in appsettings.json.");
        throw new InvalidOperationException("Connection string 'GreetingAppDB' not found.");
    }

    logger.Info("Database connection string loaded successfully.");

    // ✅ Register services
    builder.Services.AddControllers();

    // ✅ Register Redis Distributed Cache
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = builder.Configuration.GetConnectionString("Redis");
        options.InstanceName = "GreetingApp_";
    });
    builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost:6379"));


    // ✅ Register DbContext
    builder.Services.AddDbContext<GreetingAppContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services.AddScoped<Middleware.Email.SMTP>();

    // ✅ Register Business & Repository Layer

    builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();
    builder.Services.AddScoped<IGreetingRL, GreetingRL>();
    builder.Services.AddScoped<IGreetingBL, GreetingBL>();
    builder.Services.AddScoped<IUserBL, UserBL>();
    builder.Services.AddScoped<IUserRL, UserRL>();

    // ✅ Register JWT Token Helper as a Singleton
    builder.Services.AddSingleton<JwtTokenHelper>();

    // ✅ Configure Swagger
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // ✅ Enable Swagger UI in Development
    app.UseSwagger();
    app.UseSwaggerUI();

    // ✅ Middleware Configuration
    app.UseHttpsRedirection();

    // ✅ Ensure proper Authentication & Authorization order
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    logger.Info("Application started successfully.");
    app.Run();
