using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Core.Handlers;

public class PagedQuery<TResponse> : IPagedQuery<TResponse>
{
    public PagedQuery()
    {
        PageSettings = new PageSettings();
    }

    public PagedQuery(int index, int size)
    {
        PageSettings = new PageSettings(index, size);
    }

    public PagedQuery(int index, int size, int? total = null)
    {
        PageSettings = new PageSettings(index, size, total);
    }

    public PageSettings PageSettings { get; }
    
    
}