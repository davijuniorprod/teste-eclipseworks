using MediatR;

namespace TaskManager.Core.Handlers.Interfaces;

public interface ICommand : IRequest<Result<Unit>>
{
}

public interface ICommand<TResult> : IRequest<Result<TResult>>
{
}