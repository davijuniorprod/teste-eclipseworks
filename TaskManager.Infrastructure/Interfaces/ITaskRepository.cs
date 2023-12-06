using TaskManager.Domain.Entity;

namespace TaskManager.Infrastructure.Interfaces;

public interface ITaskRepository
{
    Task<Tuple<List<ProjectTask>, int>> GetPaged(string projectId, int index, int size);
    Task<ProjectTask> Get(string id);
    Task<List<ProjectTask>> GetByProjectId(string id);
    Task<ProjectTask> Update(ProjectTask task);
    Task Delete(string id);
    Task<ProjectTask> Insert(ProjectTask task);
}