using Api;
using Api.Extensions;
using Application;
using CacheService;
using Hangfire;
using JobService;
using Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddCacheServices(builder.Configuration);
builder.Services.AddJobService();
builder.Services.AddApiServices();

var app = builder.Build();

app.MapEndpoints();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    DevInitializer.Initialize(app.Services);

    app.MapOpenApi();
    app.MapScalarApiReference("/scalar-dashboard");
    app.UseHangfireDashboard("/hangfire-dashboard");
}

app.Run();
