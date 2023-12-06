namespace TaskManager.Core.Handlers.Interfaces;

public interface IPagedQueryHandler<in TRequest, TResponse> : IQueryHandler<TRequest, PagedResult<TResponse>>
    where TRequest : IQuery<PagedResult<TResponse>>
{
}