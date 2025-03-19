using System.Diagnostics;
using Humanizer;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UsersApp.Infrastructure.Logging.Decorators;

internal sealed class LoggingRequestHandlerDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingRequestHandlerDecorator<TRequest, TResponse>> _logger;
    
    public LoggingRequestHandlerDecorator(ILogger<LoggingRequestHandlerDecorator<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var commandName = typeof(TRequest).Name.Underscore();
        var stopwatch = new Stopwatch();
        stopwatch.Start();
        _logger.LogInformation("Starting handling a command {commandName}...", commandName);
        var response = await next();
        stopwatch.Stop();
        _logger.LogInformation("Complited handling a command {commandName} in {Elapsed}.", commandName, stopwatch.Elapsed);
        return response;
    }
}