namespace TaskManager.Core.Handlers;

public class PagedResult<T>
{
    public PagedResult(IEnumerable<T> items, PageSettings pageSettings)
    {
        Items = items;
        PageSettings = pageSettings;
    }

    public IEnumerable<T> Items { get; }

    public PageSettings PageSettings { get; }

    public static Result<PagedResult<T>> Success(IEnumerable<T> items, PageSettings pageSettings)
    {
        return Result.Success(new PagedResult<T>(items, pageSettings));
    }
}