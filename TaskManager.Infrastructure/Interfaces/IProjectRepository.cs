using TaskManager.Domain.Entity;
using Task = System.Threading.Tasks.Task;

namespace TaskManager.Infrastructure.Interfaces;

public interface IProjectRepository
{
    Task<Project> Get(string id);
    Task Delete(string id);
    Task<List<Project>> List();
    Task<Project> Create(Project project);
    Task<Project> Update(Project project);
}