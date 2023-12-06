using TaskManager.Application.ViewModel;
using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.Project.CreateProject;

public class CreateProjectCommand : ICommand<ProjectViewModel>
{
    public string Name { get; set; }
    public string Description { get; set; }
}