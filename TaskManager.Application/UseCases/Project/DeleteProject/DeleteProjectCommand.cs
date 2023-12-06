using TaskManager.Core.Handlers.Interfaces;

namespace TaskManager.Application.UseCases.Project.DeleteProject;

public class DeleteProjectCommand : ICommand
{
    public string Id { get; set; }
    public DeleteProjectCommand(string id) => Id = id;
}