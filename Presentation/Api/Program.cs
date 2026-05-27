using Application;
using Persistence;
using ConfigureServices = Persistence.ConfigureServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceService(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    ConfigureServices.InitializeDevDatabase(app.Services);

app.Run();
