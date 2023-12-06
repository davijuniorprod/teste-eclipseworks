using TaskManager.Domain.Entity;

namespace TaskManager.Infrastructure.Interfaces;

public interface IProjectTaskHistoryRepository
{
    Task Insert(ProjectTaskHistory projectTaskHistory);
}