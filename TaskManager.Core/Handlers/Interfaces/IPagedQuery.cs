namespace TaskManager.Core.Handlers.Interfaces;

public interface IPagedQuery<TResponse> : IQuery<PagedResult<TResponse>>
{
    PageSettings PageSettings { get; }
}