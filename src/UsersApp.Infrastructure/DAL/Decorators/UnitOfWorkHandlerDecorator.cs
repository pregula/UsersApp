using MediatR;

namespace UsersApp.Infrastructure.DAL.Decorators;

internal sealed class UnitOfWorkHandlerDecorator<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IRequestHandler<TRequest, TResponse> _requestHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkHandlerDecorator(IRequestHandler<TRequest, TResponse> requestHandler, IUnitOfWork unitOfWork)
    {
        _requestHandler = requestHandler;
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand())
        {
            return await next();
        }
        
        TResponse response = default;

        await _unitOfWork.ExecuteAsync(async () =>
        {
            response = await _requestHandler.Handle(request, cancellationToken);
        });

        return response;
    }

    private static bool IsNotCommand()
    {
        var ns = typeof(TRequest).Namespace;
        return ns != null && !ns.Contains("Command");
    }
}