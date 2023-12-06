using MediatR;

namespace TaskManager.Core.Handlers.Interfaces;

public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, Result<Unit>>
    where TRequest : IRequest<Result<Unit>>
{
}

public interface ICommandHandler<in TRequest, TResponse> : IRequestHandler<TRequest, Result<TResponse>>
    where TRequest : IRequest<Result<TResponse>>
{
}