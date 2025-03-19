using System.Reflection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UsersApp.Core.Abstractions;
using UsersApp.Infrastructure.Auth;
using UsersApp.Infrastructure.DAL;
using UsersApp.Infrastructure.DAL.Decorators;
using UsersApp.Infrastructure.Exceptions;
using UsersApp.Infrastructure.Logging;
using UsersApp.Infrastructure.Security;
using UsersApp.Infrastructure.Time;

namespace UsersApp.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ExceptionMiddleware>();
        services.AddSecurity();
        services.AddAuth(configuration);
        services.AddHttpContextAccessor();
        services
            .AddMsSql(configuration)
            .AddSingleton<IClock, Clock>();
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddOpenBehavior(typeof(UnitOfWorkHandlerDecorator<,>));
        });

        services.AddCustomLogging();
        services.AddControllers();
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        return app;
    }
}