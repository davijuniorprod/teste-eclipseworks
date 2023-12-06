using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers;

namespace TaskManager.Application.UseCases.ProjectTask.ListTasks;

public class ListTasksPagedQuery : PagedQuery<ProjectTaskViewModel>
{
    public string IdProject { get; set; }
    public int Index { get; set; }
    public int Size { get; set; }

    public ListTasksPagedQuery(string idProject, int index = 1, int size = 5): base(index, size)
    {
        IdProject = idProject;
        Index = index;
        Size = size;
    }
}