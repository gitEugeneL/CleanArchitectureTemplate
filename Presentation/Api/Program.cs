using Api;
using Api.Extensions;
using Application;
using Persistence;
using Persistence.Configuration;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApiServices();

var app = builder.Build();

app.MapEndpoints();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    DevInitializer.Initialize(app.Services);

    app.MapOpenApi();
    app.MapScalarApiReference("/scalar-dashboard");
}

app.Run();
