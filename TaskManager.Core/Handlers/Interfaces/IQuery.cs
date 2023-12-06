using MediatR;

namespace TaskManager.Core.Handlers.Interfaces;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}