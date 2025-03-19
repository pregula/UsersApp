using UsersApp.Application;
using UsersApp.Infrastructure;
using UsersApp.Infrastructure.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.UseSerilog();

var app = builder.Build();
app.UseInfrastructure();
app.Run();