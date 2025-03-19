using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using UsersApp.Infrastructure.Logging.Decorators;

namespace UsersApp.Infrastructure.Logging;

public static class Extensions
{
    internal static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        services.TryDecorate(typeof(IPipelineBehavior<,>), typeof(LoggingRequestHandlerDecorator<,>));

        return services;
    }

    public static WebApplicationBuilder UseSerilog(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
                .WriteTo
                .Console()
                .WriteTo
                .File("logs/logs.txt");
        });

        return builder;
    }
}