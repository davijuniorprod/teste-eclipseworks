using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.ProjectTask.CreateTask;

public class CreateTaskCommand : ICommand<ProjectTaskViewModel>
{
    public string IdProject { get; set; }
    public string IdUser { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
}